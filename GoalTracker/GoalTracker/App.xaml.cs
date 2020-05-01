using GoalTracker.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoalTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CalendarPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
