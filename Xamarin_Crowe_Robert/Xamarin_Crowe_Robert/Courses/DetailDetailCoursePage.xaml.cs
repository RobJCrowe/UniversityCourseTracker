using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Crowe_Robert.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin_Crowe_Robert.ViewModel;
using SQLite;

namespace Xamarin_Crowe_Robert.Courses
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailDetailCoursePage : ContentPage
	{
        classTable ct;
        public bool wentToEdit { get; set; } = false;
        public int backwards { get; set; } = -1;
		public DetailDetailCoursePage ()
		{
			InitializeComponent ();
		}
        public DetailDetailCoursePage(classTable CT)
        {
            InitializeComponent();
            ct = CT;
            backwards = 1; 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (wentToEdit == true)
            {
                wentToEdit = !wentToEdit;
                MessagingCenter.Unsubscribe<EDITCourseVM, classTable>(this, "UPDATED_CT");
            }
            if (backwards == 1)
            {
                BindingContext = ct;
                HideShowItems();
                backwards = -1;
                HideShowItems();
            }
        }

        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Course Note"
            });
        }

        private void HideShowItems()
        {
            if (ct.HasOA == true) { oaStackLayout.IsVisible = true; }
            if (ct.HasOA == false) { oaStackLayout.IsVisible = false; }
            if (ct.HasPa == true) { paStackLayout.IsVisible = true; }
            if (ct.HasPa == false) { paStackLayout.IsVisible = false; }
            bool result = string.IsNullOrEmpty(ct.optionalNotes);
            if (result == true) { oNlabel.IsVisible = false; oN.IsVisible = false; ShareOptionalNotes.IsVisible = false; }
            if (result == false) { oNlabel.IsVisible = true; oN.IsVisible = true; ShareOptionalNotes.IsVisible = true; }
        }

        private void EditCourse_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<EDITCourseVM, classTable>(this, "UPDATED_CT", (sholder, ctMessageHolder) =>
            {
                ct = ctMessageHolder;

                BindingContext = null;
                BindingContext = ct;
                HideShowItems();
            });

            wentToEdit = !wentToEdit;
            Navigation.PushAsync(new EDITCoursesPage(ct));
        }

        private async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Confirm Removal", "Are you sure you want to remove this term", "Yes", "No");
            if (response)
            {
                using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
                {
                    context.CreateTable<classTable>();
                    int rows = context.Delete(ct);
                    if (rows > 0) { await DisplayAlert("Success", "The course was removed", "Okay"); }
                    else { await DisplayAlert("Failure", "The course was not removed", "Okay"); }
                }
                await Navigation.PopAsync();
            }
        }


        private async void ShareOptionalNotes_Clicked(object sender, EventArgs e)
        {
            await ShareText(ct.optionalNotes);
        }
        
    }
}