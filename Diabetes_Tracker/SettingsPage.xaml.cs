using Diabetes_Tracker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diabetes_Tracker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
		}

        public async void Continue_Pressed(object sender, EventArgs e)
        {
            StorageHelper.WriteFile("name.dat", name.Text);

            await Navigation.PushAsync(new MainPage()
            {
                Title = "DiaLog"
            });
        }

        private void Reset_Pressed(object sender, EventArgs e)
        {
            StorageHelper.Reset();
            DisplayAlert("", "Log was cleared", "Ok");

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
    }

    
}