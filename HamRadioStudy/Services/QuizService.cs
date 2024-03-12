using HamRadioStudy.Models;
using HamRadioStudy.Utilities;

namespace HamRadioStudy.Services;

public class QuizService : IQuizService
{
    private static readonly Random _rand = new(Environment.TickCount);
    private readonly IStudyDatabase _studyDatabase;
    private readonly IQuestionService _questionService;

    private readonly Dictionary<string, string> _ylabQuizzes = new () 
    {
        { "1", "IC Regulations" },
        { "2", "Power Voltage Current Resistance" },
        { "3", "Rules & Basic Frequencies" },
        { "4", "More Frequencies & Power Limits" },
        { "5", "Repeaters & Radiophones" },
        { "6", "Licensing & CrossBorder" },
        { "7", "Antenna Location Regulations" },
        { "7B", "Safety Guidelines" },
        { "8", "Modulation & Transceiver Components" },
        { "9", "Modulation & Transceiver Components Part 2" },
        { "10", "B&width Modulation & Data" },
        { "11", "Power & Batteries" },
        { "12", "Grounding & Safety" },
        { "13", "Amps Transistors & Tubes" },
        { "14", "Energy & Gain" },
        { "15", "Inductors Capacitors & Transformers" },
        { "16", "Transmission Lines & Antenna" },
        { "17", "Advanced Antenna" },
        { "18", "Frequencies Propagation & Atmospherics" },
        { "19", "Interference & Troubleshooting" },
        { "20", "On Air Protocol" },
    };

    private readonly Dictionary<string, IList<Question>> _ylabQuizQuestions = [];

    public List<TestType> Quizes { get; }

    public QuizService(IStudyDatabase studyDatabase, IQuestionService questionService)
    {
        _studyDatabase = studyDatabase;
        _questionService = questionService;

        const int quizSize = 20;

        Quizes = 
        [
            new ("Quick Test", () => GetQuestions(quizSize)),
            new ("Unanswered Questions", async () => await GetUnansweredQuestions(quizSize)),
            new ("Review Mistakes", async () => await GetQuestionsAnsweredIncorrectly(quizSize)),
            new ("Weakest Category", async () => await GetQuestionsFromWorstSection(quizSize)),
            new ("Practice Exam", PracticeExam ),
            new ("All Questions", AllQuestions),
            new ("B-001 Regulations & Governance", () => GetQuestionsFromSection(1, quizSize)),
            new ("B-002 Operating Procedures", () => GetQuestionsFromSection(2, quizSize)),
            new ("B-003 Station Setup & Operations", () => GetQuestionsFromSection(3, quizSize)),
            new ("B-004 Amplifiers & Signals", () => GetQuestionsFromSection(4, quizSize)),
            new ("B-005 Measurements & Calculations", () => GetQuestionsFromSection(5, quizSize)),
            new ("B-006 Transmission Lines & Impedance", () => GetQuestionsFromSection(6, quizSize)),
            new ("B-007 Propagation", () => GetQuestionsFromSection(7, quizSize)),
            new ("B-008 Interference & Filtering", () => GetQuestionsFromSection(8, quizSize))
        ];

        foreach (var (key, value) in _ylabQuizzes)
        {
            Quizes.Add(new ($"YLab {key} {value}", () => GetYLabQuiz(key)));
        }
        LoadYLabQuizzes();
    }

    private void LoadYLabQuizzes()
    {
        var questions = "HamRadioStudy.Resources.Data.ylab_quizzes.txt".GetResourceLines()
            .Select(line => line.Split(';'))
            .Select(parts => new { QuizId = parts[0], QuestionId = parts[1] });

        foreach (var qid in questions)
        {
            var question = _questionService.Questions.FirstOrDefault(q => q.Id == qid.QuestionId);
            if (question == null)
                continue;

            if (!_ylabQuizQuestions.ContainsKey(qid.QuizId))
                _ylabQuizQuestions[qid.QuizId] = [];

            _ylabQuizQuestions[qid.QuizId].Add(question);
        }
    }

    /// <summary>
    /// Returns all the questions in a random order
    /// </summary>
    private IEnumerable<Question> AllQuestions() =>
        _questionService.Questions.OrderBy(_ => _rand.Next());

    private IEnumerable<Question> PracticeExam()
    {
        // Get 100 questions, one from each section/category
        var numSections = _questionService.Questions.Max(q => q.Section);
        for (var nSec = 1; nSec <= numSections; nSec++)
        {
            var section = _questionService.Questions.Where(q => q.Section == nSec);
            var numCategories = section.Max(q => q.Category);
            for (var nCat = 1; nCat <= numCategories; nCat++)
            {
                var category = section.Where(q => q.Category == nCat);
                yield return category.ElementAt(_rand.Next(category.Count()));
            }
        }
    }

    /// <summary>
    /// Returns a random selection of questions
    /// </summary>
    private IEnumerable<Question> GetQuestions(int count) =>
        _questionService.Questions.OrderBy(_ => _rand.Next()).Take(count);

    private async Task<IEnumerable<Question>> GetQuestionsAnsweredIncorrectly(int count)
    {
        var incorrect = await _studyDatabase.GetIncorrectlyAnsweredQuestions();
        return incorrect.Count == 0 ?
            GetQuestions(count) :
            incorrect
                .Select(i => _questionService.Questions.First(q => q.Id == i.QuestionId))
                .OrderBy(_ => _rand.Next())
                .Take(count);
    }

    private async Task<IEnumerable<Question>> GetUnansweredQuestions(int count)
    {
        var answered = await _studyDatabase.GetAnsweredQuestions();
        var unanswered = _questionService.Questions
            .Where(q => !answered.Any(a => a.QuestionId == q.Id))
            .OrderBy(_ => _rand.Next())
            .Take(count)
            .ToList();
        return unanswered.Count > 0 ? unanswered : await GetQuestionsAnsweredIncorrectly(count);
    }

    private async Task<IEnumerable<Question>> GetQuestionsFromWorstSection(int count)
    {
        var section = await _studyDatabase.GetWorstSection();
        return section == 0 ?
            GetQuestions(count) :
            GetQuestionsFromSection(section, count);
    }

    private IEnumerable<Question> GetQuestionsFromSection(int section, int count) =>
        _questionService.Questions
            .Where(q => q.Section == section)
            .OrderBy(_ => _rand.Next())
            .Take(count);

    private IEnumerable<Question> GetYLabQuiz(string quiz)
    {
        return _ylabQuizQuestions.TryGetValue(quiz, out var questions) ? 
            questions.OrderBy(_ => _rand.Next()) : [];
    }
}
