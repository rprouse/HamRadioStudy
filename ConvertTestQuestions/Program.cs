// Pars data/amat_basic_quest_delim.txt into a list of questions

using HamRadioStudy.Entities;
using Spectre.Console;

var rand = new Random(Environment.TickCount);
bool isEnglish = args.Length == 0;
int offset = isEnglish ? 0 : 5;

// Load basic_questions.txt into a list of questions from a resource file
var assembly = typeof(Program).Assembly;
var resourceStream = assembly.GetManifestResourceStream("HamRadioStudy.Resources.basic_questions.txt");
if (resourceStream is null)
{
    AnsiConsole.MarkupLine("❌ [Red]Resource not found[/]");
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
    int answer = OutputQuestion(question);
    if (OutputAnswer(question, answer))
    {
        break;
    }
}

static int OutputQuestion(Question question)
{
    AnsiConsole.Clear();
    AnsiConsole.MarkupLine($"[aqua]{question.Id}[/]");
    AnsiConsole.MarkupLine($"[white]{question.QuestionText}[/]");
    AnsiConsole.WriteLine();
    for (int i = 0; i < 4; i++)
    {
        AnsiConsole.MarkupLine($"  [aqua]{(char)('A' + i)}.[/] [silver]{question.Answers[i]}[/]");
    }
    AnsiConsole.WriteLine();

    char answer = AnsiConsole.Prompt(
        new TextPrompt<char>("[green]?[/] ")
            .PromptStyle("green")
            .Validate(choice =>
            {
                return char.ToLower(choice) switch
                {
                    < 'a' or > 'd' => ValidationResult.Error("[red]That's not a valid answer[/]"),
                    _ => ValidationResult.Success(),
                };
            }));

    return char.ToLower(answer) - 'a';
}

static bool OutputAnswer(Question question, int answer)
{
    AnsiConsole.Clear();
    AnsiConsole.MarkupLine($"[aqua]{question.Id}[/]");
    AnsiConsole.MarkupLine($"[white]{question.QuestionText}[/]");
    AnsiConsole.WriteLine();
    for (int i = 0; i < 4; i++)
    {
        if (i == question.CorrectAnswer)
        {
            AnsiConsole.MarkupLine($" :green_circle:[lime]{(char)('A' + i)}.[/] [green]{question.Answers[i]}[/]");
        }
        else if (i == answer && answer != question.CorrectAnswer)
        {
            AnsiConsole.MarkupLine($" :red_circle:[red]{(char)('A' + i)}.[/] [red3_1]{question.Answers[i]}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"  [aqua]{(char)('A' + i)}.[/] [silver]{question.Answers[i]}[/]");
        }
    }
    AnsiConsole.WriteLine();
    AnsiConsole.Markup("[green]>[/] ");

    var response = Console.ReadLine();

    return response?.ToLower().FirstOrDefault() == 'q';
}
