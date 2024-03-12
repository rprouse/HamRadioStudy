using HamRadioStudy.Models;

namespace HamRadioStudy;

public interface IStudyDatabase
{
    Task<int> SaveAnsweredQuestion(AnsweredQuestion answeredQuestion);

    Task<int> GetAnsweredQuestionCount();

    Task<int> GetCorrectAnswerCount();

    Task<int> GetSectionAnsweredQuestionCount(int section);

    Task<int> GetSectionCorrectAnswerCount(int section);

    Task<IList<AnsweredQuestion>> GetAnsweredQuestions();

    Task<IList<AnsweredQuestion>> GetIncorrectlyAnsweredQuestions();

    Task<int> GetWorstSection();
}
