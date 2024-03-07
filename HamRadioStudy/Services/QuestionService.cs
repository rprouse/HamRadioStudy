using HamRadioStudy.Models;
using HamRadioStudy.Utilities;

namespace HamRadioStudy.Services;

public class QuestionService : IQuestionService
{
    public IReadOnlyList<Question> Questions { get; }

    public QuestionService(bool english = true)
    {
        var offset = english ? 0 : 5;
        Questions = LoadQuestions(offset);
        LoadHints();
    }

    /// <summary>
    /// Get the number of questions in a given category
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public int CategoryQuestionCount(int category) =>
        Questions.Count(q => q.Category == category);

    /// <summary>
    /// Load basicQuestions.txt into a list of questions from a resource file
    /// </summary>
    /// <param name="offset">Offset to load Enlish (0) or French (1)</param>
    /// <returns></returns>
    private static List<Question> LoadQuestions(int offset) =>
            "HamRadioStudy.Resources.Data.basic_questions.txt".GetResourceLines()
                .Skip(1)
                .Select(line => line.Split(';'))
                .Select(parts => new Question(
                    parts[0 + offset],
                    parts[1 + offset],
                    parts[2 + offset],
                    parts.Skip(3 + offset).Take(3).ToArray()
                ))
                .ToList();

    private void LoadHints()
    {
        var hints = "HamRadioStudy.Resources.Data.hints_en.txt".GetResourceLines()
            .Select(line => line.Split(';'))
            .ToDictionary(parts => parts[0], parts => parts[1]);

        foreach (var question in Questions)
        {
            if (hints.TryGetValue(question.Id, out var hint))
            {
                question.Hint = hint;
            }
        }
    }
}