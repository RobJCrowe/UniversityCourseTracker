using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Crowe_Robert.Model;
using Xamarin_Crowe_Robert.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Crowe_Robert.Courses
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EDITCoursesPage : ContentPage
    {
        EDITCourseVM mcvm;
        public EDITCoursesPage() { InitializeComponent();}
        
        public classTable ctIN;
        public int viewIndex = -1; public string message;
        bool isEditingOa = false; bool isEditingPa = false; bool isSendingValidationMessages = false;

        public EDITCoursesPage(classTable CT)
        {
            ctIN = CT;
            InitializeComponent();
            viewIndex = 1;
        }
        private bool isAddingOa = false; private bool isAddingPa = false;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (isEditingOa == true)
            {
                isEditingOa = !isEditingOa;
                MessagingCenter.Unsubscribe<Courses.EDIT_OaAssessmentPage, EDITCourseVM>(this, "Updated_Mcvm");
            }

            if (isEditingPa== true)
            {
                MessagingCenter.Unsubscribe<Courses.EDIT_OaAssessmentPage, EDITCourseVM>(this, "Updated_Mcvm_Pa");
                isEditingPa = !isEditingPa;
            }

            if(isAddingOa == true)
            {
                isAddingOa = !isAddingOa;
                MessagingCenter.Unsubscribe<Courses.EDIT_PaAssessment, EDITCourseVM>(this, "Updated_Mcvm");
            }

            if(isAddingPa == true)
            {
                MessagingCenter.Unsubscribe<Courses.EDIT_PaAssessment, EDITCourseVM>(this, "Updated_Mcvm_Pa");
                isAddingPa = !isAddingPa; //?
            }

            if (viewIndex == 2)
            {
                BindingContext = mcvm;
                teacherPicker.SelectedItem = mcvm.SelectedTeacher;
                HasOAHasPA();
            }
                if (viewIndex == 1)
                {
                mcvm = new EDITCourseVM(ctIN);
                EDITCourseVM temp = mcvm;
                BindingContext = mcvm;
                teacherPicker.SelectedItem = mcvm.SelectedTeacher;
                HasOAHasPA();
                viewIndex = 2;
            }
        }

        #region invalidAlertMessage__Region
        void invalidAlertMessage(string index)
        {
            switch (index)
            {
                case "1":
                    DisplayAlert("Failure", "You must enter a course name.", "Okay");
                    break;
                case "2":
                    DisplayAlert("Failure", "You must pick a course status.", "Okay");
                    break;
                case "3":
                    DisplayAlert("Failure", "You must choose a course start date.", "Okay");
                    break;
                case "4":
                    DisplayAlert("Failure", "You must choose a course end date.", "Okay");
                    break;
                case "5":
                    DisplayAlert("Failure", "The start date and end date can't be the same.", "Okay");
                    break;
                case "6":
                    DisplayAlert("Failure", "Your course start date must be before the end date.", "Okay");
                    break;
                case "7":
                    DisplayAlert("Failure", "You must choose a teacher or if one is not available choose the unassigned teacher entry", "Okay");
                    break;
                case "8":
                    DisplayAlert("Success", "Your course has been added!", "Okay");
                    Navigation.PopAsync();
                    break;
                case "9":
                    DisplayAlert("Error", "Your course was not added", "Okay");
                    break;
                case "10":
                    DisplayAlert("Error", "Your course start date must be set before you can create an assessment", "Okay");
                    break;
                case "11":
                    DisplayAlert("Error", "Your course end date must be set before you can create an assessment", "Okay");
                    break;
                case "12":
                    DisplayAlert("Error", "You can't adjust the course start or end dates with an active assessments. Please remove all assessments to adjust the course start or end dates.", "Okay");
                    break;
                case "13":
                    DisplayAlert("Success", "Your course has been updated!", "Okay");
                    Navigation.PopAsync();
                    break;
                case "14":
                    DisplayAlert("Error", "Your course was not updated", "Okay");
                    break;
                case "15":
                    break;
            }
        }
        #endregion
        #region HasOaHasPa_ButtonVisibility

        public void HasOAHasPA()
        {
            if (mcvm.HasOA == true || mcvm.HasPa == true)
            {
                startCDP.IsVisible = false;
                startCourseDatePicker.Text = "Course Start: LOCKED -Remove all assessments to change.";
                endCDP.IsVisible = false;
                endCourseDatePicker.Text = "Course End: LOCKED -Remove all assessments to change.";
            }
            else if (mcvm.HasOA == false && mcvm.HasPa == false)
            {
                startCDP.IsVisible = true;
                startCourseDatePicker.Text = "Course Start:";
                endCDP.IsVisible = true;
                endCourseDatePicker.Text = "Course End:";
            }
            if (mcvm.HasOA == true)
            {
                assessmentButtonVisibility(true, AddOa, EditOa, DeleteOa);
            }
            else if (mcvm.HasOA == false)
            {
                assessmentButtonVisibility(false, AddOa, EditOa, DeleteOa);
            }

            if (mcvm.HasPa == true)
            {
                assessmentButtonVisibility(true, AddPa, EditPa, DeletePa);
            }
            else if (mcvm.HasPa == false)
            {
                assessmentButtonVisibility(false, AddPa, EditPa, DeletePa);
            }

        }

        public void assessmentButtonVisibility(bool result, Button add, Button edit, Button delete)
        {
            if (result == true) //Has Assessment
            {
                add.IsVisible = false;
                edit.IsVisible = true;
                delete.IsVisible = true;
            }
            if (result == false) //No Assessment
            {
                add.IsVisible = true; 
                edit.IsVisible = false;
                delete.IsVisible = false;
            }
        }
        #endregion

        private void SaveCourse_Clicked(object sender, EventArgs e)
        {
            mcvm.SaveTerm();
            if (mcvm.ValidationMessage != "15")
            {
                invalidAlertMessage(mcvm.ValidationMessage);
                mcvm.ValidationMessage = "15";
            }
        }

        private void AddOa_Clicked(object sender, EventArgs e)
        {
            
            bool test = mcvm.IsCourseStartEndSet();
            if (test == false) { return; }

            isAddingOa = !isAddingOa;// just changed
            MessagingCenter.Subscribe<Courses.EDIT_OaAssessmentPage, EDITCourseVM>(this, "Updated_Mcvm", (sholder, updatedMcvm) =>
            {
                mcvm = updatedMcvm; //OA
            });
            Navigation.PushAsync(new EDIT_OaAssessmentPage(mcvm, "Add Objective Assessment"));
        }

        private void EditOa_Clicked(object sender, EventArgs e)
        {
            isEditingOa = !isEditingOa; 
            MessagingCenter.Subscribe<Courses.EDIT_OaAssessmentPage, EDITCourseVM>(this, "Updated_Mcvm", (sholder, updatedMcvm) =>
            {
                mcvm = updatedMcvm; //OA
            });
            Navigation.PushAsync(new EDIT_OaAssessmentPage(mcvm, "Edit Objective Assessment", true));
        }

        private void DeleteOa_Clicked(object sender, EventArgs e)
        {
            mcvm.OaName = null; mcvm.OaNameH = null;
            mcvm.OaAnticipatedDueDate = mcvm.timeMin; mcvm.OaAntH = mcvm.timeMin;
            mcvm.OaEnd = mcvm.timeMin; mcvm.OaEndH = mcvm.timeMin;
            mcvm.HasOA = false;
            HasOAHasPA();
        }

        private void AddPa_Clicked(object sender, EventArgs e)
        {
            bool test = mcvm.IsCourseStartEndSet();
            if (test == false) { return; }

            isAddingPa = !isAddingPa; // just changed
            MessagingCenter.Subscribe<Courses.EDIT_PaAssessment, EDITCourseVM>(this, "Updated_Mcvm_Pa", (sholder, updatedMcvm) =>
            {
                mcvm = updatedMcvm; //PA
            });

            Navigation.PushAsync(new EDIT_PaAssessment(mcvm, "Add Performance Assessment"));
        }

        private void EditPa_Clicked(object sender, EventArgs e)
        {
            isEditingPa = !isEditingPa;
            MessagingCenter.Subscribe<Courses.EDIT_PaAssessment, EDITCourseVM>(this, "Updated_Mcvm_Pa", (sholder, updatedMcvm) =>
            {
                BindingContext = mcvm;
            });
            
            Navigation.PushAsync(new EDIT_PaAssessment(mcvm, "Edit Performance Assessment", true));
        }

        private void DeletePa_Clicked(object sender, EventArgs e)
        {
            mcvm.PaName = null; mcvm.PaNameH = null;
            mcvm.PaAnticipatedDueDate = mcvm.timeMin; mcvm.PaAntH = mcvm.timeMin;
            mcvm.PaEnd = mcvm.timeMin; mcvm.PaEndH = mcvm.timeMin;
            mcvm.HasPa = false;
            HasOAHasPA();
        }
    }
}