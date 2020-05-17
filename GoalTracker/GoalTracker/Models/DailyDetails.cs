using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Models
{
    public class DailyDetails
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public bool IsVisible { get; set; }
        public string Goal1 { get; set; }
        public string Goal2 { get; set; }
        public string Goal3 { get; set; }
        public string Goal4 { get; set; }
        public string Goal5 { get; set; }
    }
}
