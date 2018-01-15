using Diabetes_Tracker.Helpers;
using Diabetes_Tracker.Models;
using Newtonsoft.Json;
using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diabetes_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogPage : ContentPage
    {
        private List<LogEntry> log = new List<LogEntry>();

        public LogPage()
        {
            InitializeComponent();

            ///Read log file from storage and load to list

            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(docsPath, "log.json");

            if (File.Exists(path))
            {
                LogEntry[] arrayLog = JsonConvert.DeserializeObject<LogEntry[]>(StorageHelper.ReadFile("log.json"));
                log = new List<LogEntry>(arrayLog);
            }

            //Sort log in decending order of date and load to view

            log = log.OrderByDescending(d => d.date).ToList();
            list.ItemsSource = log;
        }

        //Function loads selected object and prepares the speech
        private void list_ItemSelected(object sender, SelectedItemChangedEventArgs e)

        {
            var entry = e.SelectedItem as LogEntry;

            string Speech = "Log for" + entry.stringDate + ".";
            Speech = Speech + "Your blood sugar level was " + entry.glucose.ToString() + "milligrams per deciliter and you did " + entry.activity.ToString() + " minutes of physical activity";

            Speak(Speech);
        }

        //Call Text To Speech
        private async void Speak(string speech)
        {
            await CrossTextToSpeech.Current.Speak(speech);
        }
    }
}