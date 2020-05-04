using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Models
{
    class Month
    {
        public string Id { get; set; }
        public string MonthName { get; set; }
        public string MonthYear { get; set; }
        public List<DailyDetails> Dailys { get; set; }
    }
}
