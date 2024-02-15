// Pars data/amat_basic_quest_delim.txt into a list of questions

using HamRadioStudy.Entities;

bool isEnglish = true;
int offset = isEnglish ? 0 : 5;

// Load basic_questions.txt into a list of questions from a resource file
var assembly = typeof(Program).Assembly;
var resourceStream = assembly.GetManifestResourceStream("HamRadioStudy.Resources.basic_questions.txt");
if (resourceStream == null)
{
    Console.WriteLine("Resource not found");
    return;
}
// var questions = new List<Question>();

using var reader = new StreamReader(resourceStream);
reader.ReadToEnd()
    .Split(new [] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries)
    .Skip(1)
    .Select(line => line.Split(';'))
    .Select(parts => new Question
    {
        QuestionId = parts[0 + offset],
        QuestionText = parts[1 + offset],
        CorrectAnswer = parts[2 + offset],
        IncorrectAnswers = parts.Skip(3 + offset).Take(3).ToList()
    })
    .ToList()
    .ForEach(Console.WriteLine);