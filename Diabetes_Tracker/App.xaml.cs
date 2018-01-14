using Diabetes_Tracker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Diabetes_Tracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (StorageHelper.FirstRun()){

                MainPage = new NavigationPage(new Intro());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage()
                {
                    Title = "DiaLog"
                });
            }



        }
  

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}