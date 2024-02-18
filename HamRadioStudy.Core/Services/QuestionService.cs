using HamRadioStudy.Core.Entities;
using HamRadioStudy.Core.Interfaces;

namespace HamRadioStudy.Core.Services;

public class QuestionService
{
    static readonly Random _rand = new (Environment.TickCount);
    private readonly List<Question> _questions;
    private readonly IStudyDatabase _studyDatabase;

    public QuestionService(IStudyDatabase studyDatabase, bool english = true)
    {
        int offset = english ? 0 : 5;

        // Load basic_questions.txt into a list of questions from a resource file
        var assembly = typeof(Question).Assembly;
        var resourceStream = assembly.GetManifestResourceStream("HamRadioStudy.Core.Resources.basic_questions.txt");
        if (resourceStream is null)
        {
            throw new InvalidDataException("Resource not found");
        }

        using var reader = new StreamReader(resourceStream);
        _questions = reader.ReadToEnd()
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .Select(line => line.Split(';'))
            .Select(parts => new Question(
                parts[0 + offset],
                parts[1 + offset],
                parts[2 + offset],
                parts.Skip(3 + offset).Take(3).ToArray()
            ))
            .ToList();
        _studyDatabase = studyDatabase;
    }

    /// <summary>
    /// Returns all the questions in a random order
    /// </summary>
    public IEnumerable<Question> AllQuestions() =>
        _questions.OrderBy(_ => _rand.Next());

    /// <summary>
    /// Returns a random selection of questions
    /// </summary>
    public IEnumerable<Question> GetQuestions(int count) =>
        _questions.OrderBy(_ => _rand.Next()).Take(count);

    public IEnumerable<Question> PracticeExam()
    {
        // Get 100 questions, one from each section/category
        int numSections = _questions.Max(q => q.Section);
        for (int nSec = 1; nSec <= numSections; nSec++)
        {
            var section = _questions.Where(q => q.Section == nSec);
            int numCategories = section.Max(q => q.Category);
            for (int nCat = 1; nCat <= numCategories; nCat++)
            {
                var category = section.Where(q => q.Category == nCat);
                yield return category.ElementAt(_rand.Next(category.Count()));
            }
        }
    }

    public async Task<IEnumerable<Question>> GetQuestionsAnsweredIncorrectly()
    {
        var incorrect = await _studyDatabase.GetIncorrectlyAnsweredQuestions();
        return incorrect.Select(i => _questions.First(q => q.Id == i.QuestionId));
    }
}
