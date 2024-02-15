using System.Text;

namespace HamRadioStudy.Entities;

public class Question
{
    public string Id { get; }
    public string QuestionText { get; }
    public string[] Answers { get; }

    public int CorrectAnswer { get; }

    public Question(string id, string question, string answer, string[] incorrectAnswers)
    {
        if (incorrectAnswers.Length != 3)
            throw new ArgumentException("There must be 3 incorrect answers", nameof(incorrectAnswers));

        var rand = new Random(Environment.TickCount);

        Id = id;
        QuestionText = question;
        Answers = new string[4];

        CorrectAnswer = rand.Next(4);
        for (int i = 0, j = 0; i < 4; i++)
        {
            if (i == CorrectAnswer)
            {
                Answers[i] = answer;
            }
            else
            {
                Answers[i] = incorrectAnswers[j++];
            }
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine(Id);
        sb.AppendLine(QuestionText);
        for (int i = 0; i < 4; i++)
        {
            char c = i == CorrectAnswer ? '*' : ' ';
            sb.AppendLine($" {c}{(char)('A' + i)}. {Answers[i]}");
        }
        return sb.ToString();
    }
}
