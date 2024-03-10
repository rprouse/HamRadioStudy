using HamRadioStudy.ViewModels;

namespace HamRadioStudy;

public partial class MainPage : ContentPage
{
    private double _width;
    private double _height;

    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        DynamicContent.BindingContext = vm;
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height); //you must call base

        if (_width == width && _height == height)
        {
            return;
        }

        _width = width;
        _height = height;

        if (width > height)
        {
            // Load landscape layout
            DynamicContent.ControlTemplate = (ControlTemplate)this.Resources["LandscapeTemplate"];
        }
        else
        {
            // Load portrait layout
            DynamicContent.ControlTemplate = (ControlTemplate)this.Resources["PortraitTemplate"];
        }
    }
}
