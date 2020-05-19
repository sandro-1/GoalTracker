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
        List<DailyDetails> dailyDetailList { get; set; }
        public List<DailyDetails> DailyDetailList
        {
            get => dailyDetailList;
            set
            {
                dailyDetailList = value;
                var arg = new PropertyChangedEventArgs(nameof(DailyDetailList));
                PropertyChanged?.Invoke(this, arg);
            }
        }
        string month { get; set; }
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

        string monthYear { get; set; }
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

        //public CalendarPageViewModel(int month, int year)
        //{
        //    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        //    MonthYear = Month.Substring(0, 3) + " " + year.ToString();
        //    ChangeMonth();
        //    OnAppearing();
        //}
        public void ArrowClick(int month, int year)
        {
            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            MonthYear = Month.Substring(0, 3) + " " + year.ToString();
            ChangeMonth();
            OnAppearing();
        }
        public CalendarPageViewModel()
        {
            //App.Database.DeleteEverythingAsync();

            Month = DateTime.Now.ToString("MMMM");
            MonthInt = DateTime.Now.Month;
            YearInt = DateTime.Now.Year;
            MonthYear = Month.Substring(0,3) + " " + YearInt.ToString();
            ChangeMonth();
            OnAppearing();

            //var result = App.Database.GetDetailAsync(Month, YearInt.ToString());

            LeftArrowClick = new Command(() =>
            {
                MonthInt--;
                if (MonthInt == 0)
                {
                    MonthInt = 12;
                    YearInt--;
                }
                ArrowClick(MonthInt, YearInt);
            });

            RightArrowClick = new Command(() =>
            {
                MonthInt++;
                if (MonthInt == 13)
                {
                    MonthInt = 1;
                    YearInt++;
                }
                ArrowClick(MonthInt, YearInt);
            });
            TapCommand = new Command<string>(OnTapped);
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
            var result = await App.Database.GetDetailAsync(Month, YearInt.ToString());
            List<DailyDetails> tempDetailList = new List<DailyDetails>();
            for (int i = 0; i < 31; i++)
            {
                tempDetailList.Add(new DailyDetails());
            }
            var dbDetailsList = result.OrderBy(d => Convert.ToInt32(d.Day)).ToList();

            int detailListIncrement = 0;
            for (int i = 0; i < tempDetailList.Count; i++)
            {
                if (dbDetailsList.ElementAtOrDefault(detailListIncrement) != null && (Convert.ToInt32(dbDetailsList[detailListIncrement].Day) - 1) == i)
                {
                    dbDetailsList[detailListIncrement].IsVisible = true;
                    tempDetailList.RemoveAt(i);
                    tempDetailList.Insert(i, dbDetailsList[detailListIncrement]);
                    detailListIncrement++;
                }
                else
                {
                    tempDetailList[i].IsVisible = false; 
                }
            }
            
            DailyDetailList = tempDetailList;

        }
        public ICommand TapCommand { get; }
        public ICommand LeftArrowClick { get; }
        public ICommand RightArrowClick { get; }
        async void OnTapped(string day)
        {
            DailyDetails details;
            DailyDetailsViewModel detailsViewModel;
            DailyDetailsPage dailyPage;            
            int dayInt = Convert.ToInt32(day);

            if (DailyDetailList.ElementAtOrDefault(dayInt - 1).Goal1 != null) //already there
            {
                details = DailyDetailList.ElementAtOrDefault(dayInt - 1);
            }
            else //creating new
            {
                details = new DailyDetails();
                details.Day = day;
                details.Month = Month;
                details.Year = YearInt.ToString();                
            }
            
            detailsViewModel = new DailyDetailsViewModel(details);
            dailyPage = new DailyDetailsPage();
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
