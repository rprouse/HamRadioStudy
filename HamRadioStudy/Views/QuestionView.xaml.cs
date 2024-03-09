using HamRadioStudy.Extensions;
using HamRadioStudy.Models;

namespace HamRadioStudy.Views;

public partial class QuestionView : ContentView
{
    private bool _answered = false;

    private readonly Style _questionStyle;
    private readonly Style _questionCorrectStyle;
    private readonly Style _questionIncorrectStyle;

    public QuestionView()
    {
        InitializeComponent();
        _questionStyle = GetStyle("Question");
        _questionCorrectStyle = GetStyle("QuestionCorrect");
        _questionIncorrectStyle = GetStyle("QuestionIncorrect");
    }

    private async void OnAnswerClicked(object sender, EventArgs e)
    {
        if (_answered ||
            BindingContext is not Question question ||
            sender is not Button button)
        {
            return;
        }

        int answer = GetAnswer(button);
        if (answer == -1) return;

        _answered = true;

        // Highlight the correct answer
        Button? correctButton = GetButton(question.CorrectAnswer);

        // Correct answer
        if (correctButton is not null)
        {
            correctButton.Style = _questionCorrectStyle;
        }

        if (question.CorrectAnswer != answer)
        {
            Button? incorrectButton = GetButton(answer);
            if (incorrectButton is not null)
                incorrectButton.Style = _questionIncorrectStyle;
        }

        // Notify the parent QuestionsPage that an answer was given
        var parent = this.GetParentOfType<QuestionsPage>();
        if (parent is not null)
            await parent.AnswerQuestion(question.CorrectAnswer == answer);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        _answered = false;

        AnswerA.Style = _questionStyle;
        AnswerB.Style = _questionStyle;
        AnswerC.Style = _questionStyle;
        AnswerD.Style = _questionStyle;
    }

    private Style GetStyle(string styleName)
    {
        if (Application.Current?.Resources.TryGetValue(styleName, out object? styleValue) == true && styleValue is Style style)
        {
            return style;
        }
        return new Style(typeof(Button));
    }

    private int GetAnswer(Button button) => button switch
    {
        _ when button == AnswerA => 0,
        _ when button == AnswerB => 1,
        _ when button == AnswerC => 2,
        _ when button == AnswerD => 3,
        _ => -1
    };

    private Button? GetButton(int answer) => answer switch
    {
        0 => AnswerA,
        1 => AnswerB,
        2 => AnswerC,
        3 => AnswerD,
        _ => null
    };
}