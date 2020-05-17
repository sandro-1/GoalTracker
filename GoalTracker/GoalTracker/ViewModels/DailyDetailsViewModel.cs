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
        DailyDetails detailsModel;
        public DailyDetails DetailsModel 
        { 
            get => detailsModel; 
            set
            {
                detailsModel = value;
                var args = new PropertyChangedEventArgs(nameof(DetailsModel));
                PropertyChanged?.Invoke(this, args);
            }                
        }

        bool labelsVisible;
        public bool LabelsVisible
        {
            get => labelsVisible;
            set
            {
                labelsVisible = value;
                var arg = new PropertyChangedEventArgs(nameof(LabelsVisible));
                PropertyChanged?.Invoke(this, arg);
            }
        }

        bool entriesVisible;
        public bool EntriesVisible
        {
            get => entriesVisible;
            set
            {
                entriesVisible = value;
                var arg = new PropertyChangedEventArgs(nameof(EntriesVisible));
                PropertyChanged?.Invoke(this, arg);
            }
        }
        public DailyDetailsViewModel(DailyDetails details)
        {
            DetailsModel = details;

            if (DetailsModel.Goal1 != null)
            {
                LabelsVisible = true;
                EntriesVisible = false;
            }
            else
            {
                LabelsVisible = false;
                EntriesVisible = true;
            }

            SaveCommand = new Command(() =>
            {
                App.Database.SaveDetailAsync(DetailsModel);                
                Application.Current.MainPage.Navigation.PopAsync();
            });
        }

        public ICommand SaveCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
}
}
