using HamRadioStudy.Models;

namespace HamRadioStudy;

public interface IStudyDatabase
{
    Task<int> SaveAnsweredQuestion(AnsweredQuestion answeredQuestion);

    Task<int> GetAnsweredQuestions();

    Task<int> GetCorrectAnswers();

    Task<int> GetSectionAnsweredQuestions(int section);

    Task<int> GetSectionCorrectAnswers(int section);

    Task<IList<AnsweredQuestion>> GetIncorrectlyAnsweredQuestions();

    Task<int> GetWorstSection();
}
