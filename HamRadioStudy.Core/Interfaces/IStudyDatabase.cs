using HamRadioStudy.Core.Models;

namespace HamRadioStudy.Core.Interfaces;

public interface IStudyDatabase
{
    Task<int> SaveAnsweredQuestion(AnsweredQuestion answeredQuestion);

    Task<int> GetAnsweredQuestions();

    Task<int> GetCorrectAnswers();

    Task<IList<AnsweredQuestion>> GetIncorrectlyAnsweredQuestions();
}
