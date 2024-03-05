using System.Diagnostics.CodeAnalysis;
using HamRadioStudy.Models;
using SQLite;

namespace HamRadioStudy
{
    // See https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-8.0
    internal class StudyDatabase : IStudyDatabase
    {
        private SQLiteAsyncConnection? _db;

        [MemberNotNull(nameof(_db))]
        private async Task Init()
        {
            if (_db is not null)
                return;

            _db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await _db.CreateTableAsync<AnsweredQuestion>();
        }

        public async Task<int> SaveAnsweredQuestion(AnsweredQuestion answeredQuestion)
        {
            await Init();
            return answeredQuestion.Id != 0 ? 
                await _db.UpdateAsync(answeredQuestion) : 
                await _db.InsertAsync(answeredQuestion);
        }

        public async Task<int> GetAnsweredQuestions()
        {
            await Init();
            return await _db.Table<AnsweredQuestion>().CountAsync();
        }

        public async Task<int> GetCorrectAnswers()
        {
            await Init();
            return await _db.Table<AnsweredQuestion>().Where(a => a.IsCorrect).CountAsync();
        }

        public async Task<int> GetSectionAnsweredQuestions(int section)
        {
            await Init();
            var result = await _db
                .Table<AnsweredQuestion>()
                .Where(a => a.Section == section)
                .ToListAsync();
            return result.GroupBy(a => a.QuestionId).Count();
        }

        public async Task<int> GetSectionCorrectAnswers(int section)
        {
            await Init();
            var result = await _db
                .Table<AnsweredQuestion>()
                .Where(a => a.Section == section && a.IsCorrect)
                .ToListAsync();
            return result.GroupBy(a => a.QuestionId).Count();
        }

        public async Task<IList<AnsweredQuestion>> GetIncorrectlyAnsweredQuestions()
        {
            await Init();
            var incorrect = await _db
                .Table<AnsweredQuestion>()
                .Where(a => !a.IsCorrect)
                .OrderBy(a => a.AnsweredAt)
                .ToListAsync();
            return incorrect.Distinct(new AnsweredQuestionComparer()).ToList();
        }

        public async Task<int> GetWorstSection()
        {
            await Init();

            var result = await _db.QueryAsync<int>("SELECT Section FROM AnsweredQuestion WHERE IsCorrect = 0 GROUP BY Section ORDER BY COUNT(*) DESC");
            var section = result.FirstOrDefault();
            return section > 0 ? section : 1;
        }
    }
}
