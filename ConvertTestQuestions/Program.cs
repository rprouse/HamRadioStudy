// Pars data/amat_basic_quest_delim.txt into a list of questions

using HamRadioStudy.Entities;

var rand = new Random(Environment.TickCount);
bool isEnglish = args.Length == 0;
int offset = isEnglish ? 0 : 5;

// Load basic_questions.txt into a list of questions from a resource file
var assembly = typeof(Program).Assembly;
var resourceStream = assembly.GetManifestResourceStream("HamRadioStudy.Resources.basic_questions.txt");
if (resourceStream == null)
{
    Console.WriteLine("Resource not found");
    return;
}

using var reader = new StreamReader(resourceStream);
var questions = reader.ReadToEnd()
    .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
    .Skip(1)
    .Select(line => line.Split(';'))
    .Select(parts => new Question(
        parts[0 + offset],
        parts[1 + offset],
        parts[2 + offset],
        parts.Skip(3 + offset).Take(3).ToArray()
    ))
    .OrderBy(q => rand.Next())
    .ToList();

foreach (var question in questions)
{
    Console.WriteLine(question);
    Console.WriteLine("Press Enter for the next question");
    Console.ReadLine();
}