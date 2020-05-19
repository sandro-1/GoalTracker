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

            if (DetailsModel.ID != 0)
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
                if (EntriesVisible == true)
                {
                    DetailsModel.Goal1Progress = DetailsModel.Goal1 != null && DetailsModel.Goal1 != "" ? "PeachPuff" : "White";
                    DetailsModel.Goal2Progress = DetailsModel.Goal2 != null && DetailsModel.Goal2 != "" ? "PeachPuff" : "White";
                    DetailsModel.Goal3Progress = DetailsModel.Goal3 != null && DetailsModel.Goal3 != "" ? "PeachPuff" : "White";
                    DetailsModel.Goal4Progress = DetailsModel.Goal4 != null && DetailsModel.Goal4 != "" ? "PeachPuff" : "White";
                    DetailsModel.Goal5Progress = DetailsModel.Goal5 != null && DetailsModel.Goal5 != "" ? "PeachPuff" : "White";
                }

                if (DetailsModel.ID != 0)
                {
                    var preUpdate = App.Database.GetDetailAsync(DetailsModel.Month, DetailsModel.Year).Result;
                    App.Database.UpdateDetailAsync(DetailsModel);
                    var postUpdate = App.Database.GetDetailAsync(DetailsModel.Month, DetailsModel.Year).Result;                    
                }
                else
                {
                    App.Database.SaveDetailAsync(DetailsModel);                    
                }
                Application.Current.MainPage.Navigation.PopAsync();
            });

            ColorChangeCommand = new Command<string>(goal =>
            {
                if (DetailsModel.Goal1Progress == "PeachPuff" && goal == "1")
                {
                    DetailsModel.Goal1Progress = "LightGreen";
                }
                else if(DetailsModel.Goal1Progress == "LightGreen" && goal == "1")
                {
                    DetailsModel.Goal1Progress = "PeachPuff";
                }
                else if (DetailsModel.Goal2Progress == "PeachPuff" && goal == "2")
                {
                    DetailsModel.Goal2Progress = "LightGreen";
                }
                else if (DetailsModel.Goal2Progress == "LightGreen" && goal == "2")
                {
                    DetailsModel.Goal2Progress = "PeachPuff";
                }
                else if (DetailsModel.Goal3Progress == "PeachPuff" && goal == "3")
                {
                    DetailsModel.Goal3Progress = "LightGreen";
                }
                else if (DetailsModel.Goal3Progress == "LightGreen" && goal == "3")
                {
                    DetailsModel.Goal3Progress = "PeachPuff";
                }
                else if (DetailsModel.Goal4Progress == "PeachPuff" && goal == "4")
                {
                    DetailsModel.Goal4Progress = "LightGreen";
                }
                else if (DetailsModel.Goal4Progress == "LightGreen" && goal == "4")
                {
                    DetailsModel.Goal4Progress = "PeachPuff";
                }
                else if (DetailsModel.Goal5Progress == "PeachPuff" && goal == "5")
                {
                    DetailsModel.Goal5Progress = "LightGreen";
                }
                else if (DetailsModel.Goal5Progress == "LightGreen" && goal == "5")
                {
                    DetailsModel.Goal5Progress = "PeachPuff";
                }

                DetailsModel = DetailsModel;
            });

        }

        public ICommand SaveCommand { get; }
        public ICommand ColorChangeCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
}
}
