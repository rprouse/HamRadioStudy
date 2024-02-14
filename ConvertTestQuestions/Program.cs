// Pars data/amat_basic_quest_delim.txt into a list of questions

using HamRadioStudy.Common.Data;

bool isEnglish = true;
int offset = isEnglish ? 0 : 5;

File.ReadAllLines("data/amat_basic_quest_delim.txt")
    .Skip(1)
    .Select(line => line.Split(';'))
    .Select(parts =>
        new Question
        {
            QuestionId = parts[0 + offset],
            QuestionText = parts[1 + offset],
            CorrectAnswer = parts[2 + offset],
            IncorrectAnswers = parts.Skip(3 + offset).Take(3).ToList()
        })
    .ToList()
    .ForEach(Console.WriteLine);
