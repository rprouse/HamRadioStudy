using System.ComponentModel;

namespace HamRadioStudy.ViewModels;

public class NavigationViewModel(int overallTotal) : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private bool _questionAnswered = false;

    /// <summary>
    /// The number of questions answered correctly
    /// </summary>
    public int Correct { get; private set; }

    /// <summary>
    /// The number of questions answered incorrectly
    /// </summary>
    public int Incorrect { get; private set; }

    /// <summary>
    /// The total number of questions answered
    /// </summary>
    public int Total => Correct + Incorrect;

    /// <summary>
    /// The total number of questions to be answered
    /// </summary>
    public int OverallTotal { get; } = overallTotal;

    /// <summary>
    /// Your current score as a percentage
    /// </summary>
    public double PercentScore => Total > 0 ? Correct / (double)Total * 100 : 0.0;

    /// <summary>
    /// Percent number of questions answered
    /// </summary>
    public double OverallPercentComplete => Total / (double)OverallTotal * 100;

    /// <summary>
    /// Has the current question been answered?
    /// </summary>
    public bool QuestionAnswered
    {
        get => _questionAnswered;
        set
        {
            _questionAnswered = value;
            OnPropertyChanged(nameof(QuestionAnswered));
        }
    }

    /// <summary>
    /// Add an answer to the score
    /// </summary>
    /// <param name="correct"></param>
    public void AddAnswer(bool correct)
    {
        if (correct) Correct++;
        else Incorrect++;

        OnPropertyChanged(nameof(Correct));
        OnPropertyChanged(nameof(Incorrect));
        OnPropertyChanged(nameof(Total));
        OnPropertyChanged(nameof(PercentScore));
        OnPropertyChanged(nameof(OverallPercentComplete));
    }

    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
