using System.Text;

namespace HamRadioStudy.Common.Data;

public class Question
{
    public string QuestionId { get; set; } = string.Empty;
    public string QuestionText { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public List<string> IncorrectAnswers { get; set; } = new List<string>();

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine(QuestionId);
        sb.AppendLine(QuestionText);
        sb.AppendLine($"  A. {CorrectAnswer}");
        sb.AppendLine($"  B. {IncorrectAnswers[0]}");
        sb.AppendLine($"  C. {IncorrectAnswers[1]}");
        sb.AppendLine($"  D. {IncorrectAnswers[2]}");
        return sb.ToString();
    }
}
