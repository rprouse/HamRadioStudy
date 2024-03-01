using HamRadioStudy.ViewModels;

namespace HamRadioStudy.Services;

public class NavigationService(IServiceProvider serviceProvider) : INavigationService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task NavigateToAsync(BaseViewModel viewModel)
    {
        var pageType = FindPageForViewModel(viewModel.GetType());
        if (pageType is null)
            throw new InvalidOperationException($"No page type for view model {viewModel.GetType()}");

        var page = (Page?)Activator.CreateInstance(pageType, viewModel);

        if (page is null)
            throw new InvalidOperationException($"Failed to create page of type {pageType}");

        await NavigateToAsync(page);
    }   

    public async Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
    {
        var pageType = FindPageForViewModel(typeof(TViewModel));
        if (pageType is null)
            throw new InvalidOperationException($"No page type for view model {typeof(TViewModel)}");

        var page = (Page?)Activator.CreateInstance(pageType);

        if (page is null)
            throw new InvalidOperationException($"Failed to create page of type {pageType}");

        await NavigateToAsync(page);
    }

    private async Task NavigateToAsync(Page page)
    {
        if (Application.Current?.MainPage is null) return;

        await Application.Current.MainPage.Navigation.PushAsync(page);
    }

    private Type? FindPageForViewModel(Type viewModelType)
    {
        // Assuming naming convention where ViewModel name is PageNameViewModel
        var pageName = viewModelType.Name.Replace("ViewModel", string.Empty);
        var pageType = Type.GetType($"HamRadioStudy.{pageName}") ?? Type.GetType($"HamRadioStudy.Views.{pageName}");
        return pageType;
    }
}

public interface INavigationService
{
    Task NavigateToAsync(BaseViewModel viewModel);

    Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
}
