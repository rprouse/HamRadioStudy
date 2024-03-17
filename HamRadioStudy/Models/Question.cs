namespace HamRadioStudy.Models;

public class Question
{
    public string Id { get; }
    public string QuestionText { get; }
    public string[] Answers { get; }

    // Compiled bindings to an item in an array like Answers[0] crashes in release builds,
    // so we need to use properties pointing to the array items.
    public string Answer1 => Answers[0];
    public string Answer2 => Answers[1];
    public string Answer3 => Answers[2];
    public string Answer4 => Answers[3];

    public int CorrectAnswer { get; }

    public string Hint { get; set; } = string.Empty;

    /// <summary>
    /// The section of the question.  This is the first 3 digits of the question id.
    /// There are 8 sections. If the Question is B-001-002, the section is 001.
    /// </summary>
    public int Section { get; }

    /// <summary>
    /// The category of the question.  This is the second 3 digits of the question id.
    /// There are a different number of categories for each section. If the Question is B-001-002, the category is 002.
    /// </summary>
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
