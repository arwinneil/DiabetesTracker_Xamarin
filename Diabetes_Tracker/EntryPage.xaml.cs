using Diabetes_Tracker.Helpers;
using Diabetes_Tracker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diabetes_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPage : ContentPage
    {
        private List<LogEntry> log = new List<LogEntry>();
        private bool _refresh;

        public EntryPage(bool refresh)
        {
            InitializeComponent();

            //Set date picker maximum date to today
            DatePicker.MaximumDate = DateTime.Now;

            //Rest of funtion loads existing log to the app

            _refresh = refresh;
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(docsPath, "log.json");

            if (File.Exists(path))
            {
                LogEntry[] arrayLog = JsonConvert.DeserializeObject<LogEntry[]>(StorageHelper.ReadFile("log.json"));
                log = new List<LogEntry>(arrayLog);

              
            }

        }

        private void UpdateLog(object sender, EventArgs e)


        {


            //Validation for zero or null blood sugar values
            if (GlucoseEntry.Text == "0" || GlucoseEntry.Text == null)
            {
                DisplayAlert("Field cannot be empty", "Please enter a value for blood sugar level", "Ok");

                return;
            }

            //Validation for negative values
            if (Convert.ToInt16(GlucoseEntry.Text) < 0 || (Convert.ToInt16(ActivityEntry.Text) < 0))
            {
                DisplayAlert("Negative values not allowed", "Please enter values greater than 0", "Ok");

                return;
            }

            //Validation for invalid blood sugar values
            if (Convert.ToInt16(GlucoseEntry.Text) < 0 || (Convert.ToInt16(GlucoseEntry.Text) > 200))
            {
                DisplayAlert("Invalid blood sugar level", "Please enter a value between 0 and 200", "Ok");

                return;
            }

            foreach (LogEntry l in log)
            {
                if (l.date.Day == DatePicker.Date.Day)
                {
                    DisplayAlert("Duplicate Entry", "An entry for " + l.stringDate + " already exists. Previous entry was overwritten.", "Ok");

                    log.Remove(l);
                    break;
                }
            }

            //Creating log object from data input
            LogEntry newEntry = new LogEntry()
            {
                glucose = Convert.ToDouble(GlucoseEntry.Text),
                activity = Convert.ToDouble(ActivityEntry.Text),
                date = DatePicker.Date,
                stringDate = DatePicker.Date.ToString("dddd, MMMM d, yyyy")
            };

            //Adding new log object to list
            log.Add(newEntry);

            //Serialise list to JSON and write to storage
            string jsonLog = JsonConvert.SerializeObject(log);
            StorageHelper.WriteFile("log.json", jsonLog);


            //Go back to main page
            if (_refresh)
            {
                Navigation.PushAsync(new MainPage()
                {
                    Title = "DiaLog"
                });
            }
            else
            {
                Navigation.PopAsync();
            }
        }
    }
}