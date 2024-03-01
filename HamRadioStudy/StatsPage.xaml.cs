using HamRadioStudy.ViewModels;

namespace HamRadioStudy
{
    public partial class StatsPage : ContentPage
    {
        public StatsPage(StatsPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}