using System.Windows.Input;
using HamRadioStudy.Models;
using HamRadioStudy.Services;

namespace HamRadioStudy.ViewModels;

public class MainPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;

    private TestType _selectedQuiz;

    public MainPageViewModel(IQuizService quizService, INavigationService navigationService, IServiceProvider serviceProvider)
    {
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;
        Quizes = quizService.Quizes;
        _selectedQuiz = Quizes[0];
    }

    public IList<TestType> Quizes { get; }

    public TestType SelectedQuiz
    { 
        get => _selectedQuiz;
        set
        {
            _selectedQuiz = value;
            OnPropertyChanged(nameof(SelectedQuiz));
        }
    }

    public ICommand TakeQuizCommand => new Command<TestType>(async (testType) =>
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
