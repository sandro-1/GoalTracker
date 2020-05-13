using GoalTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GoalTracker.ViewModels
{
    public class DailyDetailsViewModel 
    {
        public DailyDetails testModel { get; set; }        
        public DailyDetailsViewModel(string day, string month, string year)
        {
            testModel = new DailyDetails();
            testModel.Day = day;
            testModel.Month = month;
            testModel.Year = year;
        }
    }
}
