namespace HamRadioStudy.Models;

public class Question
{
    public string Id { get; }
    public string QuestionText { get; }
    public string[] Answers { get; }

    public int CorrectAnswer { get; }

    public int Section { get; }

    public int Category { get; }

    public Question(string id, string question, string answer, string[] incorrectAnswers)
    {
        if (incorrectAnswers.Length != 3)
            throw new ArgumentException("There must be 3 incorrect answers", nameof(incorrectAnswers));

        var rand = new Random(Environment.TickCount);

        Id = id;
        QuestionText = question;
        Answers = new string[4];

        // Parse out the section and category from the id
        Section = int.Parse(Id.Substring(2, 3));
        Category = int.Parse(Id.Substring(6, 3));

        CorrectAnswer = rand.Next(4);

        // Fill the answers array with the correct answer and the incorrect answers
        // The correct answer is placed at the index of CorrectAnswer
        for (int i = 0, j = 0; i < 4; i++)
        {
            Answers[i] = i == CorrectAnswer ? answer : incorrectAnswers[j++];
        }
    }

    public override string ToString() => $"{Id}: {QuestionText}";
}
