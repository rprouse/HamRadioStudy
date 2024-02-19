using HamRadioStudy.Core.Entities;

namespace HamRadioStudy.Models;

internal class TestType
{
    public string Title { get; private set; }

    private Func<IEnumerable<Question>>? GetQuestionsFunc { get; }

    private Func<Task<IEnumerable<Question>>>? GetQuestionsFuncAsync { get; }

    public TestType(string title, Func<IEnumerable<Question>> getQuestionsFunc)
    {
        Title = title;
        GetQuestionsFunc = getQuestionsFunc;
    }

    public TestType(string title, Func<Task<IEnumerable<Question>>> getQuestionsFuncAsync)
    {
        Title = title;
        GetQuestionsFuncAsync = getQuestionsFuncAsync;
    }

    public async Task<IEnumerable<Question>> GetQuestions()
    {
        if (GetQuestionsFunc is not null)
            return GetQuestionsFunc.Invoke();

        if (GetQuestionsFuncAsync is not null)
            return await GetQuestionsFuncAsync.Invoke();

        throw new InvalidOperationException("No GetQuestions function set");
    }


    public override string ToString() => Title;
}
