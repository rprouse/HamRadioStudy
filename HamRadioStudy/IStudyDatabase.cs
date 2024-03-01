using HamRadioStudy.Models;

namespace HamRadioStudy;

public interface IStudyDatabase
{
    Task<int> SaveAnsweredQuestion(AnsweredQuestion answeredQuestion);

    Task<int> GetAnsweredQuestions();

    Task<int> GetCorrectAnswers();

    Task<int> GetCategoryAnsweredQuestions(int category);

    Task<int> GetCategoryCorrectAnswers(int category);

    Task<IList<AnsweredQuestion>> GetIncorrectlyAnsweredQuestions();

    Task<int> GetWorstCategory();
}
