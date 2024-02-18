using HamRadioStudy.Core.Entities;
using HamRadioStudy.Models;
using HamRadioStudy.ViewModels;

namespace HamRadioStudy;

public partial class QuestionsPage : ContentPage
{
    private readonly Queue<Question> _questions;
    private readonly NavigationViewModel _navViewModel;
    private readonly StudyDatabase _studyDatabase = new StudyDatabase();


    public QuestionsPage(IEnumerable<Question> questions, string title)
	{
		InitializeComponent();
        Title = title;

        _questions = new Queue<Question>(questions);
        _navViewModel = new NavigationViewModel(_questions.Count);

        NavigationView.BindingContext = _navViewModel;

        // Set the first question
        if (_questions.Count > 0)
            SetNextQuestion();
    }

    public async Task NextQuestion()
    {
        if (_questions.Count == 0)
        {
            // No more questions
            await DisplayAlert("Final Score", $"Your score is {_navViewModel.PercentScore:F0}%", "OK");
            return;
        }
        SetNextQuestion();
    }

    private void SetNextQuestion()
    {
        _navViewModel.QuestionAnswered = false;
        var question = _questions.Dequeue();
        QuestionView.BindingContext = question;
    }

    public async Task AnswerQuestion(bool correct)
    {
        var question = (Question)QuestionView.BindingContext;
        if (question is not null)
        {
            AnsweredQuestion answeredQuestion = new (question, correct);
            await _studyDatabase.SaveAnsweredQuestion(answeredQuestion);
            await Navigation.PopAsync();
        }
        _navViewModel.QuestionAnswered = true;
        _navViewModel.AddAnswer(correct);
    }
}