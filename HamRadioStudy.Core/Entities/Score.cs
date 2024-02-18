using System.ComponentModel;

namespace HamRadioStudy.Core.Entities;

public class Score(int overallTotal) : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

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
    public double Percent => Total / (double)Total * 100;

    /// <summary>
    /// Your overall score as a percentage
    /// </summary>
    public double OverallPercent => Total / (double)OverallTotal * 100;

    /// <summary>
    /// Add an answer to the score
    /// </summary>
    /// <param name="correct"></param>
    public void AddAnswer(bool correct)
    {
        if (correct) Correct++;
        else Incorrect++;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Correct)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Incorrect)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Percent)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OverallPercent)));
    }
}
