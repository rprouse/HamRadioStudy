namespace HamRadioStudy.ViewModels;

public class Statistic(int correct, int answered, int total)
{
    public int CorrectAnswers { get; } = correct;
    public int AnsweredQuestions { get; } = answered;
    public int TotalQuestions { get; } = total;

    public int PercentCorrect => AnsweredQuestions > 0 ?
        (int)Math.Round((double)CorrectAnswers / AnsweredQuestions * 100) : 0;
}

public class StatsViewModel(
    Statistic overall,
    Statistic b001,
    Statistic b002,
    Statistic b003,
    Statistic b004,
    Statistic b005,
    Statistic b006,
    Statistic b007,
    Statistic b008)
{
    public Statistic Overall { get; } = overall;
    public Statistic B001 { get; } = b001;
    public Statistic B002 { get; } = b002;
    public Statistic B003 { get; } = b003;
    public Statistic B004 { get; } = b004;
    public Statistic B005 { get; } = b005;
    public Statistic B006 { get; } = b006;
    public Statistic B007 { get; } = b007;
    public Statistic B008 { get; } = b008;
}
