using CommunityToolkit.Maui.Storage;
using HamRadioStudy.Services;
using HamRadioStudy.ViewModels;

namespace HamRadioStudy;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddTransient<MainPageViewModel>()
            .AddTransient<StatsPageViewModel>()
            .AddSingleton<IFileSaver>(FileSaver.Default)
            .AddSingleton<IFolderPicker>(FolderPicker.Default)
            .AddTransient<INavigationService, NavigationService>()
            .AddSingleton<IQuestionService, QuestionService>()
            .AddSingleton<IQuizService, QuizService>()
            .AddSingleton<IStudyDatabase, StudyDatabase>()
            .AddSingleton<MainPage>();
}
