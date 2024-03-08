using HamRadioStudy.Services;

namespace HamRadioStudy.ViewModels;

public class Statistic
{
    public Statistic() { }

    public Statistic(int correct, int answered, int total)
    {
        CorrectAnswers = correct;
        AnsweredQuestions = answered;
        TotalQuestions = total;
    }

    public int CorrectAnswers { get; }
    public int AnsweredQuestions { get; }
    public int TotalQuestions { get; }

    public int PercentCorrect => AnsweredQuestions > 0 ?
        (int)Math.Round((double)CorrectAnswers / AnsweredQuestions * 100) : 0;
}

public class StatsPageViewModel(IStudyDatabase db, IQuestionService questionService) : BaseViewModel
{
    private readonly IStudyDatabase _db = db;
    private readonly IQuestionService _questionService = questionService;

    public async Task InitializeAsync()
    {
        Overall = new Statistic(await _db.GetCorrectAnswers(), await _db.GetAnsweredQuestions(), _questionService.Questions.Count);
        B001 = new Statistic(await _db.GetSectionCorrectAnswers(1), await _db.GetSectionAnsweredQuestions(1), _questionService.CategoryQuestionCount(1));
        B002 = new Statistic(await _db.GetSectionCorrectAnswers(2), await _db.GetSectionAnsweredQuestions(2), _questionService.CategoryQuestionCount(2));
        B003 = new Statistic(await _db.GetSectionCorrectAnswers(3), await _db.GetSectionAnsweredQuestions(3), _questionService.CategoryQuestionCount(3));
        B004 = new Statistic(await _db.GetSectionCorrectAnswers(4), await _db.GetSectionAnsweredQuestions(4), _questionService.CategoryQuestionCount(4));
        B005 = new Statistic(await _db.GetSectionCorrectAnswers(5), await _db.GetSectionAnsweredQuestions(5), _questionService.CategoryQuestionCount(5));
        B006 = new Statistic(await _db.GetSectionCorrectAnswers(6), await _db.GetSectionAnsweredQuestions(6), _questionService.CategoryQuestionCount(6));
        B007 = new Statistic(await _db.GetSectionCorrectAnswers(7), await _db.GetSectionAnsweredQuestions(7), _questionService.CategoryQuestionCount(7));
        B008 = new Statistic(await _db.GetSectionCorrectAnswers(8), await _db.GetSectionAnsweredQuestions(8), _questionService.CategoryQuestionCount(8));
    }

    public Statistic Overall { get; private set; } = new();
    public Statistic B001 { get; private set; } = new();
    public Statistic B002 { get; private set; } = new();
    public Statistic B003 { get; private set; } = new();
    public Statistic B004 { get; private set; } = new();
    public Statistic B005 { get; private set; } = new();
    public Statistic B006 { get; private set; } = new();
    public Statistic B007 { get; private set; } = new();
    public Statistic B008 { get; private set; } = new();
}
