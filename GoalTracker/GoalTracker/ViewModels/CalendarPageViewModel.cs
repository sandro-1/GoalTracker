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

        ICommand tapCommand;
        public CalendarPageViewModel()
        {
            //GoToDetails = new Command(() =>
            //{
            //    DailyDetailsPage dailyPage = new DailyDetailsPage();
            //    Navigation.PushAsync(dailyPage);
            //});
            tapCommand = new Command(OnTapped);
        }

        //public Command GoToDetails { get; }

        public ICommand TapCommand
        {
            get { return tapCommand; }
        }
        async void OnTapped(object s)
        {
            DailyDetailsPage dailyPage = new DailyDetailsPage();

            await Application.Current.MainPage.Navigation.PushAsync(dailyPage);
        }
        //public void TestTapped(object sender, EventArgs e) 
        //{
        //    //Navigation.PushAsync(new ContentPage { Content = new Label { Text = "Working" } }); 
        //    string test = "test";
        //}

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
