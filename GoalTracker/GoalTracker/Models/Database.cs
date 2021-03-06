﻿using SQLite;
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

        public Task<List<DailyDetails>> GetDetailAsync(string month, string year)
        {            
            return _database.Table<DailyDetails>().Where(d => d.Month == month && d.Year == year).ToListAsync();
        }

        public void SaveDetailAsync(DailyDetails detail)
        {            
            _database.InsertAsync(detail).Wait();
        }

        public void UpdateDetailAsync(DailyDetails detail)
        {   
            _database.UpdateAsync(detail).Wait();
        }

        public Task<int> DeleteEverythingAsync()
        {
            return _database.DropTableAsync<DailyDetails>();
        }
    }
}
