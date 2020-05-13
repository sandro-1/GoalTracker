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
        string Goal1 { get; set; }
        string Goal2 { get; set; }
        string Goal3 { get; set; }
        string Goal4 { get; set; }
        string Goal5 { get; set; }
        string Goal6 { get; set; }
        string Goal7 { get; set; }
        string Goal8 { get; set; }
    }
}
