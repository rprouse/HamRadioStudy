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

        public async Task<int> GetAnsweredQuestionCount()
        {
            await Init();
            var result = await _db.Table<AnsweredQuestion>().ToListAsync();
            return result.Distinct(new AnsweredQuestionComparer()).Count();
        }

        public async Task<int> GetCorrectAnswerCount()
        {
            await Init();
            var result = await _db
                .Table<AnsweredQuestion>()
                .Where(a => a.IsCorrect)
                .ToListAsync();
            return result.Distinct(new AnsweredQuestionComparer()).Count();
        }

        public async Task<int> GetSectionAnsweredQuestionCount(int section)
        {
            await Init();
            var result = await _db
                .Table<AnsweredQuestion>()
                .Where(a => a.Section == section)
                .ToListAsync();
            return result.Distinct(new AnsweredQuestionComparer()).Count();
        }

        public async Task<int> GetSectionCorrectAnswerCount(int section)
        {
            await Init();
            var result = await _db
                .Table<AnsweredQuestion>()
                .Where(a => a.Section == section && a.IsCorrect)
                .ToListAsync();
            return result.Distinct(new AnsweredQuestionComparer()).Count();
        }

        public async Task<IList<AnsweredQuestion>> GetAnsweredQuestions()
        {
            await Init();
            var incorrect = await _db
                .Table<AnsweredQuestion>()
                .OrderBy(a => a.AnsweredAt)
                .ToListAsync();
            return incorrect.Distinct(new AnsweredQuestionComparer()).ToList();
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
