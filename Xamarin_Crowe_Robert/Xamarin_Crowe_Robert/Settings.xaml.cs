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
	public partial class Settings : ContentPage
	{
        notifyTable nt;
		public Settings ()
		{
			InitializeComponent ();
            nt = notifyTable.GetNotifyTable();
            UpdateLabels();
        }

        private void SaveNotify_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
            {
                //context.CreateTable<notifyTable>();
                int rows = context.Update(nt);
                context.Close();

                if (rows > 0)
                {
                    DisplayAlert("Success", "The settings were saved.", "Okay");
                    Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Failure", "The settings did not save.", "Okay");
                }
            }
        }

        private void ToggleCa_Clicked(object sender, EventArgs e)
        {
            if(nt.notifyCourseStart == true)
            {
                nt.notifyCourseStart = false; UpdateLabels();
            }
            else
            {
                nt.notifyCourseStart = true; UpdateLabels();
            }
        }

        private void UpdateLabels()
        {
            if (nt.notifyCourseStart == true)
            {
                cStartLabel.Text = "Course Start Alerts: ENABLED";
            }
            else
            {
                cStartLabel.Text = "Course Start Alerts: DISABLED";
            }
            if (nt.notifyCourseEnd == true)
            {
                cEndLabel.Text = "Course End Alerts: ENABLED";
            }
            else
            {
                cEndLabel.Text = "Course End Alerts: DISABLED";
            }
            if (nt.notifyAnticipatedAssessment == true)
            {
                assessmentLabel.Text = "Assessment Start Alerts: ENABLED";
            }
            else
            {
                assessmentLabel.Text = "Assessment End Alerts: DISABLED";
            }
            if (nt.notifyAssessmentEnd == true)
            {
                assessmentEndLabel.Text = "Assessment End Alerts: ENABLED";
            }
            else
            {
                assessmentEndLabel.Text = "Assessment End Alerts: DISABLED";
            }
        }

        private void ToggleCend_Clicked(object sender, EventArgs e)
        {
            if (nt.notifyCourseEnd == true)
            {
                nt.notifyCourseEnd = false; UpdateLabels();

            }
            else
            {
                nt.notifyCourseEnd = true; UpdateLabels();
            }
        }

        private void ToggleAa_Clicked(object sender, EventArgs e)
        {
            if (nt.notifyAnticipatedAssessment == true)
            {
                nt.notifyAnticipatedAssessment = false; UpdateLabels();
            }
            else
            {
                nt.notifyAnticipatedAssessment = true; UpdateLabels();
            }
        }

        private void ToggleAssessmentEnd_Clicked(object sender, EventArgs e)
        {
            if (nt.notifyAssessmentEnd == true)
            {
                nt.notifyAssessmentEnd = false; UpdateLabels();
            }
            else
            {
                nt.notifyAssessmentEnd = true; UpdateLabels();
            }
        }
    }
}