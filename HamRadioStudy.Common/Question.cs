using System.Text;

namespace HamRadioStudy.Common;

public class TranslatedQuestion
{
    public string QuestionId { get; set; } = string.Empty;

    // This is the translated question text and possible answers
    // The dictionary key is the two letter language code, EN or FR
    public Dictionary<string, Question> Question { get; set; } = new();

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine(QuestionId);
        sb.AppendLine(Question["EN"].QuestionText);
        sb.AppendLine($"  A. {Question["EN"].CorrectAnswer}");
        sb.AppendLine($"  B. {Question["EN"].IncorrectAnswers[0]}");
        sb.AppendLine($"  C. {Question["EN"].IncorrectAnswers[1]}");
        sb.AppendLine($"  D. {Question["EN"].IncorrectAnswers[2]}");
        return sb.ToString();
    }
}

public class Question
{
    public string QuestionText { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public List<string> IncorrectAnswers { get; set; } = new List<string>();
}
