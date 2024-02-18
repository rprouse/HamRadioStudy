using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamRadioStudy.Models;
using SQLite;

namespace HamRadioStudy
{
    internal class StudyDatabase
    {
        SQLiteAsyncConnection _db;

        public StudyDatabase()
        {
        }

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
    }
}
