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
    class CalendarPageViewModel : ContentPage
    {
        public CalendarPageViewModel()
        {            
            TapCommand = new Command(OnTapped);
        }

        public ICommand TapCommand { get; }
        async void OnTapped()
        {
            DailyDetailsPage dailyPage = new DailyDetailsPage();
            await Application.Current.MainPage.Navigation.PushAsync(dailyPage);
        }
        
    }
}
