using GoalTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoalTracker.ViewModels
{
    public class DailyDetailsViewModel : ContentPage, INotifyPropertyChanged 
    {
        public DailyDetails detailsModel { get; set; }

        string goal1;
        public string Goal1 
        { 
            get => goal1; 
            set
            {
                goal1 = value;
                var arg = new PropertyChangedEventArgs(nameof(Goal1));
                PropertyChanged?.Invoke(this, arg);
            }
        }
        public DailyDetailsViewModel(DailyDetails details)
        {
            detailsModel = details;

            SaveCommand = new Command(() =>
            {
                detailsModel.Goal1 = Goal1;
                App.Database.SaveDetailAsync(detailsModel);                
                Application.Current.MainPage.Navigation.PopAsync();
            });
        }

        public ICommand SaveCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
}
}
