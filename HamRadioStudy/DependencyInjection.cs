using HamRadioStudy.Core.Interfaces;
using HamRadioStudy.Core.Services;
using HamRadioStudy.Services;
using HamRadioStudy.ViewModels;

namespace HamRadioStudy;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddTransient<QuestionService>()
            .AddTransient<MainPageViewModel>()
            .AddTransient<INavigationService, NavigationService>()
            .AddSingleton<IStudyDatabase, StudyDatabase>()
            .AddSingleton<MainPage>();
}
