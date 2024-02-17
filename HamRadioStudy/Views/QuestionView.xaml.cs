using System.Windows.Input;

namespace HamRadioStudy.Views;

public partial class QuestionView : ContentView
{
    public ICommand AnswerCommand { get; private set; }

    public QuestionView()
	{
		InitializeComponent();

        AnswerCommand = new Command<string>(answer =>
        {
        });
	}
}