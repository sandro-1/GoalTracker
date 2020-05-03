using GoalTracker.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        
        public ICommand TapCommand { get; }
        async void OnTapped()
        {
            DailyDetailsPage dailyPage = new DailyDetailsPage();
            await Application.Current.MainPage.Navigation.PushAsync(dailyPage);
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
