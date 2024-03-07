using HamRadioStudy.Services;
using HamRadioStudy.ViewModels;

namespace HamRadioStudy;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddTransient<MainPageViewModel>()
            .AddTransient<StatsPageViewModel>()
            .AddTransient<INavigationService, NavigationService>()
            .AddSingleton<IQuestionService, QuestionService>()
            .AddSingleton<IQuizService, QuizService>()
            .AddSingleton<IStudyDatabase, StudyDatabase>()
            .AddSingleton<MainPage>();
}
