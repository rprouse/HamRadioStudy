using HamRadioStudy.Models;

namespace HamRadioStudy.Services;

public interface IQuizService
{
    List<TestType> Quizes { get; }
}
