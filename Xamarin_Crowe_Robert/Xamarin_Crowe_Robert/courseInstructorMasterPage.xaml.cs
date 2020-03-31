using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Crowe_Robert.Model;
using Xamarin_Crowe_Robert.ViewModel;
using System.Collections.ObjectModel;

namespace Xamarin_Crowe_Robert
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class courseInstructorMasterPage : ContentPage
	{
        //List<Ci> cis;
        ObservableCollection<Ci> cisOc = new ObservableCollection<Ci>();
        //cIMasterVM _ciMasterVM;
		public courseInstructorMasterPage ()
		{
            App.currentPage = this;
			InitializeComponent ();
            //_ciMasterVM = new cIMasterVM();
            //BindingContext = _ciMasterVM;
            
		}

        private void AddCourseInstructor_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new courseInstructorAddPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cisOc = null;
            ciListView.ItemsSource = null;

            List<Ci> cis = Ci.GetCis();
            ObservableCollection<Ci> listConverter = new ObservableCollection<Ci>(cis);
            cisOc = listConverter;
            ciListView.ItemsSource = cisOc;
            
        }

        private void CiListView_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            
            
            Ci ciHolder = e.Item as Ci;
            Navigation.PushAsync(new courseInstructorDetail(ciHolder));
        }
    }
}