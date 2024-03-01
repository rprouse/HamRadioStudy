using HamRadioStudy.Core.Interfaces;
using HamRadioStudy.Core.Services;
using HamRadioStudy.Models;
using HamRadioStudy.ViewModels;

namespace HamRadioStudy;

public partial class MainPage : ContentPage
{
    private readonly QuestionService _questionService;
    private readonly IStudyDatabase _db;

    public MainPage(MainPageViewModel vm, IStudyDatabase db, QuestionService questionService)
    {
        InitializeComponent();

        BindingContext = vm;

        _db = db;
        _questionService = questionService;
    }

    private async void OnToolbarItemClicked(object sender, EventArgs e)
    {
        StatsPage page = new ();
        page.BindingContext = new StatsViewModel(
            new Statistic(await _db.GetCorrectAnswers(), await _db.GetAnsweredQuestions(), _questionService.QuestionCount),
            new Statistic(await _db.GetCategoryCorrectAnswers(1), await _db.GetCategoryAnsweredQuestions(1), _questionService.CategoryQuestionCount(1)),
            new Statistic(await _db.GetCategoryCorrectAnswers(2), await _db.GetCategoryAnsweredQuestions(2), _questionService.CategoryQuestionCount(2)),
            new Statistic(await _db.GetCategoryCorrectAnswers(3), await _db.GetCategoryAnsweredQuestions(3), _questionService.CategoryQuestionCount(3)),
            new Statistic(await _db.GetCategoryCorrectAnswers(4), await _db.GetCategoryAnsweredQuestions(4), _questionService.CategoryQuestionCount(4)),
            new Statistic(await _db.GetCategoryCorrectAnswers(5), await _db.GetCategoryAnsweredQuestions(5), _questionService.CategoryQuestionCount(5)),
            new Statistic(await _db.GetCategoryCorrectAnswers(6), await _db.GetCategoryAnsweredQuestions(6), _questionService.CategoryQuestionCount(6)),
            new Statistic(await _db.GetCategoryCorrectAnswers(7), await _db.GetCategoryAnsweredQuestions(7), _questionService.CategoryQuestionCount(7)),
            new Statistic(await _db.GetCategoryCorrectAnswers(8), await _db.GetCategoryAnsweredQuestions(8), _questionService.CategoryQuestionCount(8))
        );

        await Navigation.PushAsync(page);
    }
}
