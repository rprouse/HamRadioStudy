using System.Windows.Input;
using HamRadioStudy.Models;
using HamRadioStudy.Services;

namespace HamRadioStudy.ViewModels;

public class MainPageViewModel : BaseViewModel
{
    private readonly QuestionService _questionService;
    private readonly INavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;

    private TestType _selectedTestType;

    public MainPageViewModel(QuestionService questionService, INavigationService navigationService, IServiceProvider serviceProvider)
    {
        _questionService = questionService;
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;
        TestTypes =
        [
            new ("Quick Test", () => _questionService.GetQuestions(20)),
            new ("Review Incorrect Answers", async () => await _questionService.GetQuestionsAnsweredIncorrectly(20)),
            new ("Review Worst Category", async () => await _questionService.GetQuestionsFromWorstCategory(20)),
            new ("Practice Exam", _questionService.PracticeExam ),
            new ("All Questions", _questionService.AllQuestions)
        ];

        _selectedTestType = TestTypes[0];
    }

    public IList<TestType> TestTypes { get; }

    public TestType SelectedTestType 
    { 
        get => _selectedTestType;
        set
        {
            _selectedTestType = value;
            OnPropertyChanged(nameof(SelectedTestType));
        }
    }

    public ICommand TakeTestCommand => new Command<TestType>(async (testType) =>
    {
        var vm = new QuestionsPageViewModel(testType.Title, await testType.GetQuestions());
        await _navigationService.NavigateToAsync(vm);
    });

    public ICommand StatisticsCommand => new Command(async () =>
    {
        var vm = _serviceProvider.GetService<StatsPageViewModel>() ?? throw new InvalidOperationException("Failed to create StatsPageViewModel");
        await vm.InitializeAsync();
        await _navigationService.NavigateToAsync(vm);
    });
}
