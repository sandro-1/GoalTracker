using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Models
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<DailyDetails>().Wait();            
        }

        public Task<List<DailyDetails>> GetDetailAsync()
        {
            return _database.Table<DailyDetails>().ToListAsync();
        }

        public Task<int> SaveDetailAsync(DailyDetails detail)
        {
            return _database.InsertAsync(detail);
        }

        public Task<int> DeleteEverythingAsync()
        {
            return _database.DropTableAsync<DailyDetails>();
        }
    }
}
