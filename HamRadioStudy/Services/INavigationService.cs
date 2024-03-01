using HamRadioStudy.ViewModels;

namespace HamRadioStudy.Services;

public interface INavigationService
{
    Task NavigateToAsync(BaseViewModel viewModel);

    Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
}
