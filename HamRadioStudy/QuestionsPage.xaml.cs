using HamRadioStudy.Core.Entities;

namespace HamRadioStudy;

public partial class QuestionsPage : ContentPage
{
    private readonly Queue<Question> _questions;

    public QuestionsPage(IEnumerable<Question> questions, string title)
	{
		InitializeComponent();
        Title = title;

        _questions = new Queue<Question>(questions);

        // Set the first question
        SetQuestion();
    }

    private void SetQuestion()
    {
        var question = _questions.Dequeue();
        QuestionView.BindingContext = question;
    }
}