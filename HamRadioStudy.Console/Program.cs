// Pars data/amat_basic_quest_delim.txt into a list of questions

using HamRadioStudy.Core.Entities;
using HamRadioStudy.Core.Services;
using Spectre.Console;

try
{
    QuestionService service = new(true);

    int completed = 0;
    int correct = 0;
    foreach (var question in service.AllQuestions())
    {
        int answer = OutputQuestion(question);

        completed++;
        if (answer == question.CorrectAnswer)
            correct++;

        if (OutputAnswer(question, answer, correct, completed))
            break;
    }
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex);
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

static bool OutputAnswer(Question question, int answer, int correct, int completed)
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
    AnsiConsole.MarkupLine($"[red][[{correct}/{completed}]] {(double)correct / completed * 100.0:F0}%[/]");
    AnsiConsole.WriteLine();
    AnsiConsole.Markup("[green]>[/] ");

    var response = Console.ReadLine();

    return response?.ToLower().FirstOrDefault() == 'q';
}
