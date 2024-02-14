using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace HamRadioStudy.Common.Data;

public class TranslatedQuestion
{
    public string QuestionId { get; set; } = string.Empty;

    public Question EnglishQuestion { get; set; } = new();
    public Question FrenchQuestion { get; set; } = new();

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine(QuestionId);
        sb.AppendLine(EnglishQuestion.QuestionText);
        sb.AppendLine($"  A. {EnglishQuestion.CorrectAnswer}");
        sb.AppendLine($"  B. {EnglishQuestion.IncorrectAnswers[0]}");
        sb.AppendLine($"  C. {EnglishQuestion.IncorrectAnswers[1]}");
        sb.AppendLine($"  D. {EnglishQuestion.IncorrectAnswers[2]}");
        return sb.ToString();
    }
}

public class Question
{
    public string QuestionText { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public List<string> IncorrectAnswers { get; set; } = new List<string>();
}
