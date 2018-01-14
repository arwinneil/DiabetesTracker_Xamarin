using Diabetes_Tracker.Helpers;
using Diabetes_Tracker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            _refresh = refresh;
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(docsPath, "log.json");

            if (File.Exists(path))
            {
                LogEntry[] arrayLog = JsonConvert.DeserializeObject<LogEntry[]>(StorageHelper.ReadFile("log.json"));
                log = new List<LogEntry>(arrayLog);

                DatePicker.MaximumDate = DateTime.Now;
            }
        }

        private void UpdateLog(object sender, EventArgs e)
        {
            if (GlucoseEntry.Text == "0" || GlucoseEntry.Text == null)
            {
                DisplayAlert("Field cannot be empty", "Please enter a value for blood sugar level", "Ok");

                return;
            }

            if (Convert.ToInt16(GlucoseEntry.Text) < 0 || (Convert.ToInt16(ActivityEntry.Text) < 0))
            {
                DisplayAlert("Negative values not allowed", "Please enter values greater than 0", "Ok");

                return;
            }

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

            LogEntry newEntry = new LogEntry()
            {
                glucose = Convert.ToDouble(GlucoseEntry.Text),
                activity = Convert.ToDouble(ActivityEntry.Text),
                date = DatePicker.Date,
                stringDate = DatePicker.Date.ToString("dddd, MMMM d, yyyy")
            };

            log.Add(newEntry);

            string jsonLog = JsonConvert.SerializeObject(log);

            StorageHelper.WriteFile("log.json", jsonLog);

            if (_refresh)
            {
                var existingPages = Navigation.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    Navigation.RemovePage(page);
                }

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