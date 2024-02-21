using System.Diagnostics.CodeAnalysis;
using HamRadioStudy.Core.Interfaces;
using HamRadioStudy.Core.Models;
using SQLite;

namespace HamRadioStudy
{
    // See https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-8.0
    internal class StudyDatabase : IStudyDatabase
    {
        SQLiteAsyncConnection? _db;

        [MemberNotNull(nameof(_db))]
        async Task Init()
        {
            if (_db is not null)
                return;

            _db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await _db.CreateTableAsync<AnsweredQuestion>();
        }

        public async Task<int> SaveAnsweredQuestion(AnsweredQuestion answeredQuestion)
        {
            await Init();
            if (answeredQuestion.Id != 0)
                return await _db.UpdateAsync(answeredQuestion);
            else
                return await _db.InsertAsync(answeredQuestion);
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

        public async Task<int> GetWorstCategory()
        {
            await Init();

            return await _db.ExecuteAsync("SELECT Category FROM AnsweredQuestion WHERE IsCorrect = 0 GROUP BY Category ORDER BY COUNT(*) DESC LIMIT 1");
        }
    }
}
