using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Crowe_Robert.Model;

namespace Xamarin_Crowe_Robert
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class courseInstructorEditPage : ContentPage
	{
        private Ci ci;
		public courseInstructorEditPage ()
		{
			InitializeComponent ();
		}
        public courseInstructorEditPage(Ci Ci)
        {
            InitializeComponent();
            ci = Ci;
            ciName.Text = ci.ciName;
            ciPhone.Text = ci.ciPhone;
            ciEmail.Text = ci.ciEmail;
        }

        private bool isValid()
        {
            bool one = string.IsNullOrWhiteSpace(ciName.Text);
            if(one == true)
            {
                DisplayAlert("Failure", "The course instructor's name cannot be left blank", "Okay");
                return false;
            }
            bool two = string.IsNullOrWhiteSpace(ciPhone.Text);
            if (two == true)
            {
                DisplayAlert("Failure", "The course instructor's phone cannot be left blank", "Okay");
                return false;
            }
            bool three = string.IsNullOrWhiteSpace(ciEmail.Text);
            if (three == true)
            {
                DisplayAlert("Failure", "The course instructor's email cannot be left blank", "Okay");
                return false;
            }
            return true;
        }


        private void SaveEditCi_Clicked(object sender, EventArgs e)
        {
            bool result = isValid();
            if (result == false) { return; }
            if(ci.ciName != ciName.Text || ci.ciPhone != ciPhone.Text || ci.ciEmail != ciName.Text)
            {
                ci.ciName = ciName.Text; ci.ciPhone = ciPhone.Text; ci.ciEmail = ciEmail.Text;
                using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
                {
                    context.CreateTable<Ci>();
                    int rows = context.Update(ci);
                    if (rows > 0) { DisplayAlert("Sucess", "Your update has been made.", "Okay"); }
                    else { DisplayAlert("Failure", "Your update has failed", "Okay"); }

                    }
                MessagingCenter.Send<Ci>(ci, "Update");
                Navigation.PopAsync();
            }
            else { DisplayAlert("Please try again", "No values have changed", "Okay"); }
        }
    }

}