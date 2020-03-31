using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Crowe_Robert.Model;
using Xamarin_Crowe_Robert.Courses;

namespace Xamarin_Crowe_Robert.Terms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class detailTermPage : ContentPage
	{
        private termTable tt;
		public detailTermPage ()
		{
			InitializeComponent ();
		}
        public detailTermPage(termTable TT)
        {
            InitializeComponent();
            tt = TT;
            setLabels(tt);
        }

        private void setLabels(termTable ttHolder)
        {
            termNameLabel.Text =  "NAME:  " + ttHolder.termName;
            termStartLabel.Text = "START: " + ttHolder.termStart.ToString("yyyy-MM-dd");
            termEndLabel.Text = "END:   " + ttHolder.termEnd.ToString("yyyy-MM-dd");
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(isUpdating == true)
            {
                MessagingCenter.Unsubscribe<editTermPage ,termTable>(this, "Update");
                isUpdating = !isUpdating;
            }
            setLabels(tt);
        }

        private bool isUpdating = false; termTable ttHolder;
        private void UpdateTerm_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<editTermPage ,termTable>(this, "Update", (sHolder , TT) =>
            {
                ttHolder = TT;
            });
            isUpdating = !isUpdating;
            Navigation.PushAsync(new editTermPage(tt));
        }

        private async void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Confirm Removal", "Are you sure you want to remove this term", "Yes", "No");
            if (response)
            {
                using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
                {
                    context.CreateTable<termTable>();
                    int rows = context.Delete(tt);
                    if (rows > 0) { await DisplayAlert("Sucess", "The term was removed", "Okay"); }
                    else { await DisplayAlert("Failure", "The term failed to be deleted", "Okay"); }
                }
                await Navigation.PopAsync();
            }
        }

        private void ManageTerms_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CoursesDetailPage(tt));
        }
       
    }
}