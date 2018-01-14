using Diabetes_Tracker.Helpers;
using Diabetes_Tracker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace Diabetes_Tracker
{
    public partial class MainPage : ContentPage

    {
        private List<LogEntry> log = new List<LogEntry>();

        public MainPage()
        {
            NavigationPage.SetHasBackButton(this, false);

            InitializeComponent();
            Today.Text = "You have no log for today!";

            InitializeGestures();
            InitialiseLog();

            Hello.Text = "Hello, " + StorageHelper.ReadFile("name.dat");
        }

        public void InitializeGestures()
        {
            LogsFrame.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    openLog();
                })
            });

            entryFrame.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    openEntry();
                })
            });

            SettingsFrame.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    openSettings();
                })
            });

           AboutUsFrame.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    Device.OpenUri(new Uri("http://mysteryinc.epizy.com/aboutUs.html"));
                })
            });
        }

        public void InitialiseLog()
        {
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(docsPath, "log.json");

            if (File.Exists(path))
            {
                LogEntry[] arrayLog = JsonConvert.DeserializeObject<LogEntry[]>(StorageHelper.ReadFile("log.json"));
                log = new List<LogEntry>(arrayLog);
            }

            log = log.OrderByDescending(d => d.date).ToList();

            foreach (LogEntry l in log)
            {
                if (l.date.Day == DateTime.Now.Day)
                {
                    Today.Text = "Log done for today!";
                }
            }
        }

        public async void openLog()
        {
            await Navigation.PushAsync(new LogPage()
            {
                Title = "All logs - Select To Listen to Log"
            });
        }

        public async void openEntry()
        {
            bool refresh = !(Today.Text == "Log done for today!");

            await Navigation.PushAsync(new EntryPage(refresh)
            {
                Title = "New Entry"
            });
        }

        public async void openSettings()
        {
            await Navigation.PushAsync(new SettingsPage
            {
                Title = "Settings"
            });
        }
    }
}