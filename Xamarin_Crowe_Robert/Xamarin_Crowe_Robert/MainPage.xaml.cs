using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_Crowe_Robert.Terms;
using Xamarin_Crowe_Robert.Model;
using SQLite;

namespace Xamarin_Crowe_Robert
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<termTable> termsOc = new ObservableCollection<termTable>();
        
        public MainPage()
        {
            App.currentPage = this;
            InitializeComponent();
        }

        private void Settings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings());
        }

        private void ManageCi_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new courseInstructorMasterPage());
        }

        private void AddTerm_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new addTermPage());
        }

        private void TermListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            termTable termHolder = e.Item as termTable;
            Navigation.PushAsync(new detailTermPage(termHolder));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            termsOc = null;
            termListView.ItemsSource = null;

            List<termTable> terms = termTable.GetTerms(); 

            ObservableCollection<termTable> listConverter = new ObservableCollection<termTable>(terms);
            termsOc = listConverter;
            termListView.ItemsSource = termsOc;
            
        }
    }
}
