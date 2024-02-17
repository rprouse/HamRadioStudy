using HamRadioStudy.Core.Services;

namespace HamRadioStudy
{
    public partial class MainPage : ContentPage
    {
        private readonly QuestionService _questionService = new QuestionService(true);

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAllQuestionsClicked(object sender, EventArgs e)
        {
            // Navigate to the QuestionsPage
            var page = new QuestionsPage(_questionService.AllQuestions, "All Questions");
            await Navigation.PushAsync(page);
        }
    }

}
