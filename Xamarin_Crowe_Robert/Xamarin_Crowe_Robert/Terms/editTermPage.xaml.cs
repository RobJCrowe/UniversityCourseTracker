using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Crowe_Robert.Model;
using Xamarin_Crowe_Robert.ViewModel;

namespace Xamarin_Crowe_Robert.Terms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class editTermPage : ContentPage
	{
        private termTable tt;
        private TermEditVM teVM;
        public editTermPage ()
		{
			InitializeComponent ();
		}

        public editTermPage(termTable TT)
        {
            InitializeComponent();

            tt = TT;
            teVM = new TermEditVM(TT);
            BindingContext = teVM;

            //MessagingCenter.Subscribe<string>(this, "GoBack", (result) =>
            //{
            //    if (result == "back") { Navigation.PopAsync(); }
            //});
            //MessagingCenter.Subscribe<string>(this, "ErrorMsg", (result) =>
            //{
            //    ErrorMsg(result);
            //});
        }

        private void ErrorMsg(string name)
        {
            switch (name)
            {
                case "1":
                    DisplayAlert("Failure", "You must enter a term name.", "Okay");
                    break;
                case "2":
                    DisplayAlert("Failure", "Course start and end dates must not be the same.", "Okay");
                    break;
                case "3":
                    DisplayAlert("Failure", "Your course start date must be before the end date.", "Okay");
                    break;
                case "4":
                    DisplayAlert("Failure", "Your term was not updated.", "Okay");
                    break;
                case "5":
                    DisplayAlert("Success", "Your term has been updated.", "Okay");
                    MessagingCenter.Send<editTermPage, termTable>(this, "Update", tt );
                    Navigation.PopAsync();
                    break;
                case "6":
                    break;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = teVM;

        }
        
        private void SaveEditTerm_Clicked(object sender, EventArgs e)
        {
            teVM.UpdateTerm();
            ErrorMsg(teVM.message);
        }
        
    }
}