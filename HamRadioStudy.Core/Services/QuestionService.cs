using HamRadioStudy.Core.Entities;

namespace HamRadioStudy.Core.Services;

public class QuestionService
{
    static readonly Random _rand = new(Environment.TickCount);
    private readonly List<Question> _questions;

    public QuestionService(bool english)
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
    }

    /// <summary>
    /// Returns all the questions in a random order
    /// </summary>
    public IEnumerable<Question> AllQuestions =>
        _questions.OrderBy(_ => _rand.Next());
}
