using HamRadioStudy.ViewModels;

namespace HamRadioStudy.Services;

public class NavigationService : INavigationService
{
    public async Task NavigateToAsync(BaseViewModel viewModel)
    {
        var pageType = NavigationService.FindPageForViewModel(viewModel.GetType()) ?? throw new InvalidOperationException($"No page type for view model {viewModel.GetType()}");
        var page = (Page?)Activator.CreateInstance(pageType, viewModel) ?? throw new InvalidOperationException($"Failed to create page of type {pageType}");
        await NavigationService.NavigateToAsync(page);
    }   

    public async Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
    {
        var pageType = NavigationService.FindPageForViewModel(typeof(TViewModel)) ?? throw new InvalidOperationException($"No page type for view model {typeof(TViewModel)}");
        var page = (Page?)Activator.CreateInstance(pageType) ?? throw new InvalidOperationException($"Failed to create page of type {pageType}");
        await NavigationService.NavigateToAsync(page);
    }

    private static async Task NavigateToAsync(Page page)
    {
        if (Application.Current?.MainPage is null) return;

        await Application.Current.MainPage.Navigation.PushAsync(page);
    }

    private static Type? FindPageForViewModel(Type viewModelType)
    {
        // Assuming naming convention where ViewModel name is PageNameViewModel
        var pageName = viewModelType.Name.Replace("ViewModel", string.Empty);
        var pageType = Type.GetType($"HamRadioStudy.{pageName}") ?? Type.GetType($"HamRadioStudy.Views.{pageName}");
        return pageType;
    }
}
