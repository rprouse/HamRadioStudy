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
            button.BackgroundColor = Color.FromArgb("#70EE9C");
        }
        else
        {
            // Incorrect answer
            button.BackgroundColor = Color.FromArgb("#DB3A34");

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
                correctButton.BackgroundColor = Color.FromArgb("#70EE9C");
            }
        }
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        _answered = false;

        var buttonColor = Application.Current?.RequestedTheme switch
        {
            AppTheme.Dark => Color.FromArgb("#9BD1E5"),
            AppTheme.Light => Color.FromArgb("#484041"),
            _ => Color.FromArgb("#9BD1E5")
        };

        AnswerA.BackgroundColor = buttonColor;
        AnswerB.BackgroundColor = buttonColor;
        AnswerC.BackgroundColor = buttonColor;
        AnswerD.BackgroundColor = buttonColor;
    }
}