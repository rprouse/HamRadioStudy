using HamRadioStudy.Models;

namespace HamRadioStudy.Services;

public interface IQuestionService
{
    IReadOnlyList<Question> Questions { get; }
    int CategoryQuestionCount(int category);
}
