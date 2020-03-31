using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Crowe_Robert.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace Xamarin_Crowe_Robert
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class courseInstructorAddPage : ContentPage
	{
		public courseInstructorAddPage ()
		{
			InitializeComponent ();
		}

        private bool isValid()
        {
            bool one = string.IsNullOrWhiteSpace(ciAddNameEntry.Text);
            if (one == true)
            {
                DisplayAlert("Failure", "The course instructor's name cannot be left blank", "Okay");
                return false;
            }
            bool two = string.IsNullOrWhiteSpace(ciAddPhoneEntry.Text);
            if (two == true)
            {
                DisplayAlert("Failure", "The course instructor's phone cannot be left blank", "Okay");
                return false;
            }
            bool three = string.IsNullOrWhiteSpace(ciAddEmailEntry.Text);
            if (three == true)
            {
                DisplayAlert("Failure", "The course instructor's email cannot be left blank", "Okay");
                return false;
            }
            return true;
        }

        private void CiAddSave_Clicked(object sender, EventArgs e)
        {
            bool result = isValid();
            if (result == false) { return; }
            Ci ci = new Ci()
            {
                ciName = ciAddNameEntry.Text,
                ciPhone = ciAddPhoneEntry.Text,
                ciEmail = ciAddPhoneEntry.Text
            };
            using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
            {
                context.CreateTable<Ci>();
                int rows = context.Insert(ci);
                context.Close();

                if (rows > 0)
                {
                    DisplayAlert("Success", "The course instructor was added.", "Cancel");
                }
                else
                {
                    DisplayAlert("Failure", "The course instructor was not added.", "Cancel");
                }
                Navigation.PopAsync();
            }
        }
    }
}