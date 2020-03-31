using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Crowe_Robert.ViewModel;
using Xamarin_Crowe_Robert.Courses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Crowe_Robert.Courses
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaAssessment : ContentPage
	{
        private MasterCourseVM mcvm;
        private bool isUpdate;
        public PaAssessment()
        {
            InitializeComponent();
        }

        public PaAssessment(MasterCourseVM MCVM, string title)
        {
            InitializeComponent();
            paTitleLabel.Text = title;
            mcvm = MCVM;
            BindingContext = mcvm;
        }

        public PaAssessment(MasterCourseVM MCVM, string title, bool IsUpdate)
        {
            InitializeComponent();
            paTitleLabel.Text = title;
            isUpdate = IsUpdate;
            mcvm = MCVM;
            mcvm.PaNameH = mcvm.PaName;
            mcvm.PaAntH = mcvm.PaAnticipatedDueDate;
            mcvm.PaEndH = mcvm.PaEnd;
            BindingContext = mcvm;
        }
        private bool isValid(string testType)
        {
            if (testType == "oa")
            {
                if (string.IsNullOrEmpty(mcvm.OaNameH))
                { invalidAlertMessage(1); return false; }
                if (mcvm.OaAntH == mcvm.timeMin)
                { invalidAlertMessage(2); return false; }
                if (mcvm.OaEndH == mcvm.timeMin)
                { invalidAlertMessage(3); return false; }
                if (isDateValid(mcvm.OaAntH, mcvm.OaEndH)) { return false; }
                return true;
            }

            if (testType == "pa")
            {
                if (string.IsNullOrEmpty(mcvm.PaNameH))
                { invalidAlertMessage(1); return false; }
                if (mcvm.PaAntH == mcvm.timeMin)
                { invalidAlertMessage(2); return false; }
                if (mcvm.PaEndH == mcvm.timeMin)
                { invalidAlertMessage(3); return false; }
                if (isDateValid(mcvm.PaAntH, mcvm.PaEndH)) { return false; }
                return true;
            }
            return false;

            bool isDateValid(DateTime ant, DateTime end)
            {
                if (ant == end)
                {
                    invalidAlertMessage(4);
                    return true;
                }
                if (ant.CompareTo(end) > 0)
                {
                    invalidAlertMessage(5);
                    return true;
                }
                return false;
            }

            void invalidAlertMessage(int index)
            {
                switch (index)
                {
                    case 1:
                        DisplayAlert("Failure", "You must enter an assessment name.", "Okay");
                        break;
                    case 2:
                        DisplayAlert("Failure", "You must choose an anticipated due date.", "Okay");
                        break;
                    case 3:
                        DisplayAlert("Failure", "You must choose an end date.", "Okay");
                        break;
                    case 4:
                        DisplayAlert("Failure", "The anticipated due date and end date can't be the same.", "Okay");
                        break;
                    case 5:
                        DisplayAlert("Failure", "Your anticipated due date must be before the end date.", "Okay");
                        break;
                }
            }
        }

        private void SaveAssessment_Clicked(object sender, EventArgs e)
        {
            bool result = isValid("pa");
            if (result == false) { return; }
            mcvm.PaName = mcvm.PaNameH;
            mcvm.PaAnticipatedDueDate = mcvm.PaAntH;
            mcvm.PaEnd = mcvm.PaEndH;
            mcvm.HasPa = true;

            MessagingCenter.Send<Courses.PaAssessment, MasterCourseVM>(this, "Updated_Mcvm_Pa", mcvm);
            Navigation.PopAsync();

        }
    }
}
