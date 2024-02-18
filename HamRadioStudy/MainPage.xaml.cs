using HamRadioStudy.Core.Services;

namespace HamRadioStudy
{
    public partial class MainPage : ContentPage
    {
        private readonly QuestionService _questionService = new QuestionService(true);

        public MainPage()
        {
            InitializeComponent();
            NumberOfQuestionsPicker.SelectedIndex = 0;
        }

        private async void OnPracticeExamClicked(object sender, EventArgs e)
        {
            var page = new QuestionsPage(_questionService.PracticeExam(), "Practice Exam");
            await Navigation.PushAsync(page);
        }

        private async void OnQuickTestClicked(object sender, EventArgs e)
        {
            int numQuestions = (NumberOfQuestionsPicker.SelectedIndex + 1) * 10;
            var page = new QuestionsPage(_questionService.GetQuestions(numQuestions), "Quick Test");
            await Navigation.PushAsync(page);
        }

        private async void OnAllQuestionsClicked(object sender, EventArgs e)
        {
            var page = new QuestionsPage(_questionService.AllQuestions(), "All Questions");
            await Navigation.PushAsync(page);
        }
    }

}
