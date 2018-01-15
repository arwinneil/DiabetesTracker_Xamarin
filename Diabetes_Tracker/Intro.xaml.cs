using Diabetes_Tracker.Helpers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diabetes_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Intro : ContentPage
    {
        public Intro()
        {
            InitializeComponent();
        }
        
        //Write name to  storage
        public async void Continue_Pressed(object sender, EventArgs e)
        {
            StorageHelper.WriteFile("name.dat", name.Text);

            await Navigation.PushAsync(new MainPage()
            {
                Title = "DiaLog"
            });
        }
    }
}