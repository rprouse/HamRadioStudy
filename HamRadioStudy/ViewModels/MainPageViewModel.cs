using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using HamRadioStudy.Models;
using HamRadioStudy.Services;

namespace HamRadioStudy.ViewModels;

public class MainPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IStudyDatabase _database;
    private readonly IFileSaver _fileSaver;
    private TestType _selectedQuiz;

    public MainPageViewModel(IQuizService quizService, INavigationService navigationService, IServiceProvider serviceProvider, IStudyDatabase database, IFileSaver fileSaver)
    {
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;
        Quizes = quizService.Quizes;
        _selectedQuiz = Quizes[0];
        _database = database;
        _fileSaver = fileSaver;
    }

    public IList<TestType> Quizes { get; }

    public TestType SelectedQuiz
    { 
        get => _selectedQuiz;
        set
        {
            _selectedQuiz = value;
            OnPropertyChanged(nameof(SelectedQuiz));
        }
    }

    public ICommand TakeQuizCommand => new Command<TestType>(async (testType) =>
    {
        var vm = new QuestionsPageViewModel(testType.Title, await testType.GetQuestions());
        await _navigationService.NavigateToAsync(vm);
    });

    public ICommand StatisticsCommand => new Command(async () =>
    {
        var vm = _serviceProvider.GetService<StatsPageViewModel>() ?? throw new InvalidOperationException("Failed to create StatsPageViewModel");
        await vm.InitializeAsync();
        await _navigationService.NavigateToAsync(vm);
    });

    public ICommand OpenWebsiteCommand => 
        new Command<string>(async (uri) => await OpenWebsite(uri));

    public ICommand BackupDatabaseCommand => new Command(async () =>
    {
        await _database.Close();
        using var stream = new FileStream(Constants.DatabasePath, FileMode.Open, FileAccess.Read);
        var fileSaverResult = await _fileSaver.SaveAsync(Constants.DatabaseFilename, stream);
        if (fileSaverResult.IsSuccessful)
        {
            await Toast.Make($"Backed up to: {fileSaverResult.FilePath}").Show();
        }
        else
        {
            await Toast.Make($"Backup failed: {fileSaverResult.Exception.Message}").Show();
        }
    });

    private static async Task OpenWebsite(string uri)
    {
        try
        {
            await Launcher.OpenAsync(new Uri(uri));
        }
        catch (Exception ex)
        {
            // Handle any exceptions
            Debug.WriteLine(ex.ToString());
        }
    }    
}
