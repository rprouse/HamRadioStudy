using HamRadioStudy.Core.Entities;
using HamRadioStudy.ViewModels;

namespace HamRadioStudy;

public partial class QuestionsPage : ContentPage
{
    private readonly Queue<Question> _questions;
    private readonly NavigationViewModel _navViewModel;

    public QuestionsPage(IEnumerable<Question> questions, string title)
	{
		InitializeComponent();
        Title = title;

        _questions = new Queue<Question>(questions);
        _navViewModel = new NavigationViewModel(_questions.Count);

        NavigationView.BindingContext = _navViewModel;

        // Set the first question
        NextQuestion();
    }

    public void NextQuestion()
    {
        if (_questions.Count == 0)
        {
            // No more questions
            // TODO: Show the score
            return;
        }

        _navViewModel.QuestionAnswered = false;
        var question = _questions.Dequeue();
        QuestionView.BindingContext = question;
    }

    public void AnswerQuestion(bool correct)
    {
        _navViewModel.QuestionAnswered = true;
        _navViewModel.AddAnswer(correct);
    }
}