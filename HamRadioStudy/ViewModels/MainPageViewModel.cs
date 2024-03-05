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
            new ("Review Mistakes", async () => await _questionService.GetQuestionsAnsweredIncorrectly(20)),
            new ("Practice Weakest Category", async () => await _questionService.GetQuestionsFromWorstSection(20)),
            new ("B-001 Regulations & Governance", () => _questionService.GetQuestionsFromSection(1, 20)),
            new ("B-002 Operating Procedures", () => _questionService.GetQuestionsFromSection(2, 20)),
            new ("B-003 Station Setup & Operations", () => _questionService.GetQuestionsFromSection(3, 20)),
            new ("B-004 Amplifiers & Signals", () => _questionService.GetQuestionsFromSection(4, 20)),
            new ("B-005 Measurements & Calculations", () => _questionService.GetQuestionsFromSection(5, 20)),
            new ("B-006 Transmission Lines & Impedance", () => _questionService.GetQuestionsFromSection(6, 20)),
            new ("B-007 Propagation", () => _questionService.GetQuestionsFromSection(7, 20)),
            new ("B-008 Interference & Filtering", () => _questionService.GetQuestionsFromSection(8, 20)),
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
