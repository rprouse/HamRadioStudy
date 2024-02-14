// Pars data/amat_basic_quest_delim.txt into a list of questions

using HamRadioStudy.Common;

File.ReadAllLines("data/amat_basic_quest_delim.txt")
    .Skip(1)
    .Select(line => line.Split(';'))
    .Select(parts => 
        new TranslatedQuestion()
        {
            QuestionId = parts[0],
            Question = new()
            {
                ["EN"] = new Question
                {
                    QuestionText = parts[1],
                    CorrectAnswer = parts[2],
                    IncorrectAnswers = parts.Skip(3).Take(3).ToList()
                },
                ["FR"] = new Question
                {
                    QuestionText = parts[6],
                    CorrectAnswer = parts[7],
                    IncorrectAnswers = parts.Skip(8).Take(3).ToList()
                }
            }
        })
    .ToList()
    .ForEach(Console.WriteLine);
