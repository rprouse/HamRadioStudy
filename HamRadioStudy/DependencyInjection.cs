using HamRadioStudy.Services;
using HamRadioStudy.ViewModels;

namespace HamRadioStudy;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddTransient<QuestionService>()
            .AddTransient<MainPageViewModel>()
            .AddTransient<StatsPageViewModel>()
            .AddTransient<INavigationService, NavigationService>()
            .AddSingleton<IStudyDatabase, StudyDatabase>()
            .AddSingleton<MainPage>();
}
