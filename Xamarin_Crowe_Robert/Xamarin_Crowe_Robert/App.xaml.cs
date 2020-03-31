using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Xamarin_Crowe_Robert.Model;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xamarin_Crowe_Robert
{
    public partial class App : Application
    {
        public static ContentPage currentPage;
        public static string DatabaseLocation = string.Empty;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            
        }
        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            DatabaseLocation = databaseLocation;
            
        }

        protected override void OnStart()
        {
            using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
            {
                var tableExists = context.GetTableInfo("table_NotifyTable");
                if (tableExists.Count == 0)
                { OnFirstRun.loadDummyData(); }
            };
            HeadsUp notification = new HeadsUp();
            notification.SetProps();
            notification.RunAllNotifications();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
