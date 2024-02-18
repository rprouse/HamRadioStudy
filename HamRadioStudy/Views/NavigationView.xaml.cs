using HamRadioStudy.Extensions;

namespace HamRadioStudy.Views;

public partial class NavigationView : ContentView
{
    public NavigationView()
    {
        InitializeComponent();
    }

    private void OnNextClicked(object sender, EventArgs e)
    {
        this.GetParentOfType<QuestionsPage>()?.NextQuestion();
    }
}