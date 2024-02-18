using System.Windows.Input;
using HamRadioStudy.Core.Entities;

namespace HamRadioStudy.Views;

public partial class QuestionView : ContentView
{
    private bool _answered = false;

    private readonly Color _correctColor;
    private readonly Color _incorrectColor;
    private readonly Color _buttonColor;

    public QuestionView()
	{
		InitializeComponent();

        // I would prefer to get these out of the colors.xaml resource dictionary
        // but I'm not sure how to do that in a ContentView
        _buttonColor = Application.Current?.RequestedTheme switch
        {
            AppTheme.Dark => Color.FromArgb("#9BD1E5"),
            AppTheme.Light => Color.FromArgb("#484041"),
            _ => Color.FromArgb("#9BD1E5")
        };

        _correctColor = Application.Current?.RequestedTheme switch
        {
            AppTheme.Dark => Color.FromArgb("#70EE9C"),
            AppTheme.Light => Color.FromArgb("#4CA169"),
            _ => Color.FromArgb("#70EE9C")
        };

        _incorrectColor = Color.FromArgb("#DB3A34");
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
            button.BackgroundColor = _correctColor;
        }
        else
        {
            // Incorrect answer
            button.BackgroundColor = _incorrectColor;

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
                correctButton.BackgroundColor = _correctColor;
            }
        }
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        _answered = false;

        AnswerA.BackgroundColor = _buttonColor;
        AnswerB.BackgroundColor = _buttonColor;
        AnswerC.BackgroundColor = _buttonColor;
        AnswerD.BackgroundColor = _buttonColor;
    }
}