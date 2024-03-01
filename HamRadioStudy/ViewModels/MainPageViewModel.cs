using System.Windows.Input;
using HamRadioStudy.Core.Interfaces;
using HamRadioStudy.Core.Services;
using HamRadioStudy.Models;
using HamRadioStudy.Services;

namespace HamRadioStudy.ViewModels;

public class MainPageViewModel : BaseViewModel
{
    private readonly QuestionService _questionService;
    private readonly INavigationService _navigationService;
    private readonly IStudyDatabase _db;

    private TestType _selectedTestType;

    public MainPageViewModel(IStudyDatabase db, QuestionService questionService, INavigationService navigationService)
    {
        _db = db;
        _questionService = questionService;
        _navigationService = navigationService;

        TestTypes = new List<TestType>
        {
            new TestType("Quick Test", () => _questionService.GetQuestions(20)),
            new TestType("Review Incorrect Answers", async () => await _questionService.GetQuestionsAnsweredIncorrectly(20)),
            new TestType("Review Worst Category", async () => await _questionService.GetQuestionsFromWorstCategory(20)),
            new TestType("Practice Exam", _questionService.PracticeExam ),
            new TestType("All Questions", _questionService.AllQuestions)
        };

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
}
