using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;

namespace Xamarin_Crowe_Robert.Droid
{
    [Activity(Label = "Xamarin_Crowe_Robert", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState); //ADDED by rjc 2019-07-31
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            string dbName = "termManager_db.sqlite";
            string aPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fPath = Path.Combine(aPath, dbName);
            LoadApplication(new App(fPath));
        }
    }
}