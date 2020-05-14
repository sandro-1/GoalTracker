using GoalTracker.Models;
using GoalTracker.ViewModels;
using GoalTracker.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
namespace GoalTracker
{
    class CalendarPageViewModel : ContentPage, INotifyPropertyChanged
    {
        public string month { get; set; }
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

        public string monthYear { get; set; }
        public string MonthYear
        {
            get => monthYear;
            set
            {
                monthYear = value;
                var arg = new PropertyChangedEventArgs(nameof(MonthYear));
                PropertyChanged?.Invoke(this, arg);
            }
        }
        public int YearInt { get; set; }
        public int MonthInt { get; set; }

        bool showDate1 { get; set; }
        public bool ShowDate1 
        { 
            get => showDate1; 
            set 
            { 
                showDate1 = value;
                var arg = new PropertyChangedEventArgs(nameof(ShowDate1));
                PropertyChanged?.Invoke(this, arg);
            } 
        }

        bool showDate2 { get; set; }
        public bool ShowDate2
        {
            get => showDate2;
            set
            {
                showDate2 = value;
                var arg = new PropertyChangedEventArgs(nameof(ShowDate2));
                PropertyChanged?.Invoke(this, arg);
            }
        }

        bool showDate3 { get; set; }
        public bool ShowDate3
        {
            get => showDate3;
            set
            {
                showDate3 = value;
                var arg = new PropertyChangedEventArgs(nameof(ShowDate3));
                PropertyChanged?.Invoke(this, arg);
            }
        }

        public CalendarPageViewModel()
        {                        
            Month = DateTime.Now.ToString("MMMM");
            MonthInt = DateTime.Now.Month;
            YearInt = DateTime.Now.Year;
            MonthYear = Month.Substring(0,3) + " " + YearInt.ToString();
            ChangeMonth();

            //App.Database.DeleteEverythingAsync();
            var result = App.Database.GetDetailAsync();
            var resultList = result.Result;
            var testDetail = result.Result.FirstOrDefault();

            LeftArrowClick = new Command(() =>
            {
                MonthInt--;
                if (MonthInt == 0)
                {
                    MonthInt = 12;
                    YearInt--;
                }
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(MonthInt);
                MonthYear = Month.Substring(0, 3) + " " + YearInt.ToString();
                ChangeMonth();
            });

            RightArrowClick = new Command(() =>
            {
                MonthInt++;
                if (MonthInt == 13)
                {
                    MonthInt = 1;
                    YearInt++;
                }
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(MonthInt);
                MonthYear = Month.Substring(0, 3) + " " + YearInt.ToString();
                ChangeMonth();
            });
            TapCommand = new Command<string>(OnTapped);

            //OnAppearing();
        }

        void ChangeMonth()
        {
            var daysInCurrentMonth = DateTime.DaysInMonth(YearInt, MonthInt);

            //adjust visibility on calendar days depending on month
            if (daysInCurrentMonth == 31)
            {
                ShowDate1 = true;
                ShowDate2 = true;
                ShowDate3 = true;
            }
            else if (daysInCurrentMonth == 30)
            {
                ShowDate1 = true;
                ShowDate2 = true;
                ShowDate3 = false;
            }
            else
            {
                if (daysInCurrentMonth == 28)
                {
                    ShowDate1 = false;
                    ShowDate2 = false;
                    ShowDate3 = false;
                }
                else //leap year
                {
                    ShowDate1 = true;
                    ShowDate2 = false;
                    ShowDate3 = false;
                }
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var result = await App.Database.GetDetailAsync();            
        }
        public ICommand TapCommand { get; }
        public ICommand LeftArrowClick { get; }
        public ICommand RightArrowClick { get; }
        async void OnTapped(string day)
        {
            //await App.Database.SaveDetailAsync(dailyDetailTestInput);
            //var result = await App.Database.GetDetailAsync();

            //creating new
            DailyDetails details = new DailyDetails();
            details.Day = day;
            details.Month = Month;
            details.Year = YearInt.ToString();
            DailyDetailsViewModel detailsViewModel = new DailyDetailsViewModel(details);
            DailyDetailsPage dailyPage = new DailyDetailsPage();
            dailyPage.BindingContext = detailsViewModel;
            dailyPage.Disappearing += (sender, e) => //calls onappearing after detailpage is popped
            { 
                this.OnAppearing(); 
            };
            await Application.Current.MainPage.Navigation.PushAsync(dailyPage);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
