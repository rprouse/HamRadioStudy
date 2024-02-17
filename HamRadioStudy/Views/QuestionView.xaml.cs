using System.Windows.Input;
using HamRadioStudy.Core.Entities;

namespace HamRadioStudy.Views;

public partial class QuestionView : ContentView
{
    private bool _answered = false;

    public QuestionView()
	{
		InitializeComponent();
	}

    private void OnAnswerClicked(object sender, EventArgs e)
    {
        if (BindingContext is not Question question ||
            sender is not Button button ||
            _answered)
        {
            return;
        }

        _answered = true;

        // Check which answer was clicked
        // by comparing the sender to the buttons
        int answer = button switch
        {
            _ when button == AnswerA => 0,
            _ when button == AnswerB => 1,
            _ when button == AnswerC => 2,
            _ when button == AnswerD => 3,
            _ => -1
        };

        if (question.CorrectAnswer == answer)
        {
            // Correct answer
            button.BackgroundColor = Colors.LightGreen;
        }
        else
        {
            // Incorrect answer
            button.BackgroundColor = Colors.LightCoral;

            // Highlight the correct answer
            Button? correctButton = question.CorrectAnswer switch
            {
                0 => AnswerA,
                1 => AnswerB,
                2 => AnswerC,
                3 => AnswerD,
                _ => null
            };

            if (correctButton is not null)
            {
                correctButton.BackgroundColor = Colors.LightGreen;
            }
        }
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        _answered = false;

        AnswerA.BackgroundColor = Colors.LightBlue;
        AnswerB.BackgroundColor = Colors.LightBlue;
        AnswerC.BackgroundColor = Colors.LightBlue;
        AnswerD.BackgroundColor = Colors.LightBlue;
    }
}