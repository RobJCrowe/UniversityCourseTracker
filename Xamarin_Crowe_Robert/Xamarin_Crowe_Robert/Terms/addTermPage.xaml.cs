using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Crowe_Robert.Model;

namespace Xamarin_Crowe_Robert.Terms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class addTermPage : ContentPage
	{
        termTable tt = new termTable();
		public addTermPage ()
		{
			InitializeComponent ();
		}

        private bool IsTermValid()
        {
            if (string.IsNullOrEmpty(lbAddTermName.Text)){
                DisplayAlert("Failure", "You must enter a term name.", "Okay"); return false;
            }
            if (startDP.Date == endDP.Date)
            {
                DisplayAlert("Failure", "Course start and end dates must not be the same.", "Okay"); return false;
            }
            if(startDP.Date.CompareTo(endDP.Date) > 0)
            {
                DisplayAlert("Failure", "Your course start date must be before the end date.", "Okay"); return false;
            }
            return true;
        }

        private void AddTermSave_Clicked(object sender, EventArgs e)
        {
            bool result = IsTermValid();
            if (result == false)
            {
                return;
            }
            tt.termName = lbAddTermName.Text;
            tt.termStart = startDP.Date;
            tt.termEnd = endDP.Date;
            using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
            {
                context.CreateTable<termTable>();
                int rows = context.Insert(tt);
                context.Close();

                if (rows > 0)
                {
                    DisplayAlert("Success", "The term was added.", "Okay");
                }
                else
                {
                    DisplayAlert("Failure", "The term was not added.", "Okay");
                }
            }
                Navigation.PopAsync();
            }
    }
}