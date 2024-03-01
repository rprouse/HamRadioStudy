using System.Diagnostics.CodeAnalysis;

namespace HamRadioStudy.Models;

public class AnsweredQuestion
{
    public int Id { get; set; }
    public string QuestionId { get; set; } = string.Empty;
    public int Section { get; set; }
    public int Category { get; set; }
    public bool IsCorrect { get; set; }
    public DateTimeOffset AnsweredAt { get; set; }

    public AnsweredQuestion() { }

    public AnsweredQuestion(Question question, bool isCorrect)
    {
        QuestionId = question.Id;
        Section = question.Section;
        Category = question.Category;
        IsCorrect = isCorrect;
        AnsweredAt = DateTimeOffset.Now;
    }
}

public class AnsweredQuestionComparer : IEqualityComparer<AnsweredQuestion>
{ 
    public bool Equals(AnsweredQuestion? x, AnsweredQuestion? y) =>
        x?.QuestionId == y?.QuestionId;

    public int GetHashCode([DisallowNull] AnsweredQuestion obj) =>
        obj.QuestionId.GetHashCode();
}