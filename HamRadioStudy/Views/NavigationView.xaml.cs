using HamRadioStudy.Extensions;

namespace HamRadioStudy.Views;

public partial class NavigationView : ContentView
{
    public NavigationView()
    {
        InitializeComponent();
    }

    private async void OnNextClicked(object sender, EventArgs e)
    {
        var parent = this.GetParentOfType<QuestionsPage>();
        if (parent is not null)
            await parent.NextQuestion();
    }
}