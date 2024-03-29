using HamRadioStudy.Models;
using HamRadioStudy.ViewModels;

namespace HamRadioStudy;

public partial class QuestionsPage : ContentPage
{
    private readonly Queue<Question> _questions;
    private Question? _currentQuestion;
    private readonly NavigationViewModel _navViewModel;
    private readonly StudyDatabase _studyDatabase = new();


    public QuestionsPage(QuestionsPageViewModel vm)
	{
		InitializeComponent();
        Title = vm.Title;

        _questions = new Queue<Question>(vm.Questions);
        _navViewModel = new NavigationViewModel(_questions.Count);

        NavigationView.BindingContext = _navViewModel;

        // Set the first question
        if (_questions.Count > 0)
            _currentQuestion = SetNextQuestion();
    }

    public async Task NextQuestion()
    {
        if (_questions.Count == 0)
        {
            // No more questions
            await DisplayAlert("Final Score", $"Your score is {_navViewModel.PercentScore:F0}%", "OK");
            await Navigation.PopAsync();
            return;
        }
        _currentQuestion = SetNextQuestion();
    }

    private Question SetNextQuestion()
    {
        _navViewModel.QuestionAnswered = false;
        var question = _questions.Dequeue();
        QuestionView.BindingContext = question;
        return question;
    }

    public async Task AnswerQuestion(bool correct)
    {
        var question = (Question)QuestionView.BindingContext;
        if (question is not null)
        {
            AnsweredQuestion answeredQuestion = new (question, correct);
            await _studyDatabase.SaveAnsweredQuestion(answeredQuestion);
        }
        _navViewModel.QuestionAnswered = true;
        _navViewModel.AddAnswer(correct);
    }

    private async void OnHintClicked(object sender, EventArgs e)
    {
        if (_currentQuestion is not null)
        {
            await DisplayAlert("Hint", _currentQuestion.Hint, "OK");
        }
    }
}