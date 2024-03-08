namespace HamRadioStudy.ViewModels;

public class StatisticViewModel
{
    public StatisticViewModel(string title, int correct, int answered, int total)
    {
        Title = title;
        CorrectAnswers = correct;
        AnsweredQuestions = answered;
        TotalQuestions = total;
    }

    public string Title { get; }
    public int CorrectAnswers { get; }
    public int AnsweredQuestions { get; }
    public int TotalQuestions { get; }

    public int PercentCorrect => AnsweredQuestions > 0 ?
        (int)Math.Round((double)CorrectAnswers / AnsweredQuestions * 100) : 0;
}
