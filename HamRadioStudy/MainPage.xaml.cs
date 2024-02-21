using HamRadioStudy.Core.Services;
using HamRadioStudy.Models;

namespace HamRadioStudy;

public partial class MainPage : ContentPage
{
    private readonly QuestionService _questionService;
    private readonly StudyDatabase _studyDatabase = new ();

    public MainPage()
    {
        InitializeComponent();
        _questionService = new QuestionService(_studyDatabase);

        TestTypePicker.ItemsSource = new List<TestType>
        {
            new TestType("Quick Test", () => _questionService.GetQuestions(20)),
            new TestType("Review Incorrect Answers", async () => await _questionService.GetQuestionsAnsweredIncorrectly(20)),
            new TestType("Review Worst Category", async () => await _questionService.GetQuestionsFromWorstCategory(20)),
            new TestType("Practice Exam", _questionService.PracticeExam ),
            new TestType("All Questions", _questionService.AllQuestions)
        };
        TestTypePicker.SelectedIndex = 0;
    }

    private async void OnTakeTestClicked(object sender, EventArgs e)
    {
        var testType = TestTypePicker.SelectedItem as TestType;
        if (testType is null) return;
        var page = new QuestionsPage(await testType.GetQuestions(), testType.Title);
        await Navigation.PushAsync(page);
    }
}
