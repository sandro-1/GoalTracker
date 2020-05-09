using GoalTracker.Models;
using GoalTracker.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
namespace GoalTracker
{
    class CalendarPageViewModel : ContentPage, INotifyPropertyChanged
    {
        string month;
        public string Month
        {
            get => month;
            set
            {
                month = value;
                var arg = new PropertyChangedEventArgs(nameof(Month));
                PropertyChanged?.Invoke(this, arg);
            }
        }
        public CalendarPageViewModel()
        {            
            TapCommand = new Command(OnTapped);
            Month = DateTime.Now.ToString("MMMM");
        }
        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    var result = await App.Database.GetCalendarAsync();
        //    Month = result.FirstOrDefault().MonthList.FirstOrDefault().MonthName;
        //}
        public ICommand TapCommand { get; }
        async void OnTapped()
        {
            DailyDetails dailyDetailTestInput = new DailyDetails
            {
                Day = "Day 1 Test",
                Month = "Month 1 Test",
                Year = "Year 1 Test"
            };

            await App.Database.SaveDetailAsync(dailyDetailTestInput);
            var result = await App.Database.GetDetailAsync();

            //DailyDetailsPage dailyPage = new DailyDetailsPage();
            //await Application.Current.MainPage.Navigation.PushAsync(dailyPage);
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
