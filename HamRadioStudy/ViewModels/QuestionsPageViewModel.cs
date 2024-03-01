using HamRadioStudy.Core.Entities;

namespace HamRadioStudy.ViewModels;

public class QuestionsPageViewModel(string title, IEnumerable<Question> questions) : BaseViewModel
{
    public string Title { get; } = title;

    public IEnumerable<Question> Questions { get; } = questions;
}
