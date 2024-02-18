using HamRadioStudy.Core.Entities;

namespace HamRadioStudy;

public partial class QuestionsPage : ContentPage
{
    private readonly Queue<Question> _questions;
    private readonly Score _score;

    public QuestionsPage(IEnumerable<Question> questions, string title)
	{
		InitializeComponent();
        Title = title;

        _questions = new Queue<Question>(questions);
        _score = new Score(_questions.Count);

        NavigationView.BindingContext = _score;

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

        var question = _questions.Dequeue();
        QuestionView.BindingContext = question;
    }

    public void AnswerQuestion(bool correct)
    {
        _score.AddAnswer(correct);
    }
}