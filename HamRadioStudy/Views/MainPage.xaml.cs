using HamRadioStudy.ViewModels;

namespace HamRadioStudy;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
