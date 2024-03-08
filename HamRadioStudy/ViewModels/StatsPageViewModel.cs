using HamRadioStudy.Services;

namespace HamRadioStudy.ViewModels;

public class StatsPageViewModel(IStudyDatabase db, IQuestionService questionService) : BaseViewModel
{
    private readonly IStudyDatabase _db = db;
    private readonly IQuestionService _questionService = questionService;

    public async Task InitializeAsync()
    {
        Overall = new StatisticViewModel("📈 Overall Statistics", await _db.GetCorrectAnswers(), await _db.GetAnsweredQuestions(), _questionService.Questions.Count);
        Sections.AddRange([
            new StatisticViewModel("B-001 Regulations & Governance", await _db.GetSectionCorrectAnswers(1), await _db.GetSectionAnsweredQuestions(1), _questionService.CategoryQuestionCount(1)),
            new StatisticViewModel("B-002 Operating Procedures", await _db.GetSectionCorrectAnswers(2), await _db.GetSectionAnsweredQuestions(2), _questionService.CategoryQuestionCount(2)),
            new StatisticViewModel("B-003 Station Setup & Operations", await _db.GetSectionCorrectAnswers(3), await _db.GetSectionAnsweredQuestions(3), _questionService.CategoryQuestionCount(3)),
            new StatisticViewModel("B-004 Amplifiers & Signals", await _db.GetSectionCorrectAnswers(4), await _db.GetSectionAnsweredQuestions(4), _questionService.CategoryQuestionCount(4)),
            new StatisticViewModel("B-005 Measurements & Calculations", await _db.GetSectionCorrectAnswers(5), await _db.GetSectionAnsweredQuestions(5), _questionService.CategoryQuestionCount(5)),
            new StatisticViewModel("B-006 Transmission Lines & Impedance", await _db.GetSectionCorrectAnswers(6), await _db.GetSectionAnsweredQuestions(6), _questionService.CategoryQuestionCount(6)),
            new StatisticViewModel("B-007 Propagation", await _db.GetSectionCorrectAnswers(7), await _db.GetSectionAnsweredQuestions(7), _questionService.CategoryQuestionCount(7)),
            new StatisticViewModel("B-008 Interference & Filtering", await _db.GetSectionCorrectAnswers(8), await _db.GetSectionAnsweredQuestions(8), _questionService.CategoryQuestionCount(8)),
        ]);
    }

    public StatisticViewModel? Overall { get; private set; }
    public List<StatisticViewModel> Sections { get; } = new List<StatisticViewModel>();
}
