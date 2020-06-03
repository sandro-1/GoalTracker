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
        public int FirstOfMonthPlacement { get; set; }
        GridLength lastRow { get; set; }
        public GridLength LastRow
        {
            get => lastRow;
            set
            {
                lastRow = value;
                var arg = new PropertyChangedEventArgs(nameof(LastRow));
                PropertyChanged?.Invoke(this, arg);
            }
        }        
        public void ArrowClick(int month, int year)
        {
            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            MonthYear = Month.Substring(0, 3) + " " + year.ToString();            
            OnAppearing();
        }
        public CalendarPageViewModel()
        {
            Month = DateTime.Now.ToString("MMMM");
            MonthInt = DateTime.Now.Month;
            YearInt = DateTime.Now.Year;            
            MonthYear = Month.Substring(0,3) + " " + YearInt.ToString();

            OnAppearing();

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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            DateTime firstOfTheMonthInt = new DateTime(YearInt, MonthInt, 1);
            var firstOfTheMonthString = firstOfTheMonthInt.DayOfWeek.ToString();

            if (firstOfTheMonthString == "Sunday")
            {
                FirstOfMonthPlacement = 0;
            }
            else if (firstOfTheMonthString == "Monday")
            {
                FirstOfMonthPlacement = 1;
            }
            else if (firstOfTheMonthString == "Tuesday")
            {
                FirstOfMonthPlacement = 2;
            }
            else if (firstOfTheMonthString == "Wednesday")
            {
                FirstOfMonthPlacement = 3;
            }
            else if (firstOfTheMonthString == "Thursday")
            {
                FirstOfMonthPlacement = 4;
            }
            else if (firstOfTheMonthString == "Friday")
            {
                FirstOfMonthPlacement = 5;
            }
            else
            {
                FirstOfMonthPlacement = 6;
            }

            var result = await App.Database.GetDetailAsync(Month, YearInt.ToString());
            List<DailyDetails> tempDetailList = new List<DailyDetails>();            
            for (int i = 0; i < 42; i++)
            {
                tempDetailList.Add(new DailyDetails());
                tempDetailList[i].Goal1Progress = "White";
                tempDetailList[i].Goal2Progress = "White";
                tempDetailList[i].Goal3Progress = "White";
                tempDetailList[i].Goal4Progress = "White";
                tempDetailList[i].Goal5Progress = "White";
            }
            var dbDetailsList = result.OrderBy(d => Convert.ToInt32(d.Day)).ToList();
            int daysInMonth = DateTime.DaysInMonth(YearInt, MonthInt);
            int detailListIncrement = 0;
            int dayTracker = 1;
            for (int i = 0; i < tempDetailList.Count; i++)
            {
                if (i < FirstOfMonthPlacement)
                {
                    tempDetailList[i].IsVisible = false;                    
                }
                else if (FirstOfMonthPlacement + daysInMonth <= i)
                {
                    tempDetailList[i].IsVisible = false;                    
                }
                else
                {
                    tempDetailList[i].Day = dayTracker.ToString();                    
                    if (dbDetailsList.ElementAtOrDefault(detailListIncrement) != null && 
                        Convert.ToInt32(dbDetailsList[detailListIncrement].Day) - 1 + FirstOfMonthPlacement == i)
                    {                    
                        tempDetailList.RemoveAt(i);
                        tempDetailList.Insert(i, dbDetailsList[detailListIncrement]);
                        detailListIncrement++;
                    }
                    tempDetailList[i].IsVisible = true;
                    dayTracker++;                   
                }
            }
            if (tempDetailList[35].Day == null)
            {
                LastRow = new GridLength(0);
            }
            else
            {
                LastRow = new GridLength(1, GridUnitType.Star);
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

            if (DailyDetailList.ElementAtOrDefault(dayInt + FirstOfMonthPlacement - 1).Goal1 != null) //already there
            {
                details = DailyDetailList.ElementAtOrDefault(dayInt + FirstOfMonthPlacement - 1);
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
