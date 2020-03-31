using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Crowe_Robert.Model;

namespace Xamarin_Crowe_Robert
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class courseInstructorDetail : ContentPage
	{
        private Ci ci;
        
        public courseInstructorDetail()
        {
            InitializeComponent();
        }

		public courseInstructorDetail (Ci Ci)
		{
			InitializeComponent ();
            ci = Ci;
            //setLabels(ci);
            
            //MessagingCenter.Subscribe<Ci>(this, "Update", (ciOnUpdate) => 
            //{
            //    setLabels(ciOnUpdate);
            //});
		}
        private void setLabels(Ci ciHolder)
        {
            nameLabel.Text = "NAME:    " + ciHolder.ciName;
            phoneLabel.Text = "PHONE:  " + ciHolder.ciPhone;
            emailLabel.Text = "EMAIL:  " + ciHolder.ciEmail;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(IsEditing == true)
            {
                MessagingCenter.Unsubscribe<Ci>(this, "Update");
                IsEditing = !IsEditing;

            }
            setLabels(ci);
        }
        private bool IsEditing = false;
        private void UpdateCI_Clicked(object sender, EventArgs e)
        {
            if (ci.courseInstructorId == 1)
            {
                DisplayAlert("Permission Denied", "You can not edit the unassigned mentor course instructor entry.", "Okay");
                return;
            }
            IsEditing = !IsEditing;
            MessagingCenter.Subscribe<Ci>(this, "Update", (ciOnUpdate) =>
            {
                ci = ciOnUpdate;
            });
            Navigation.PushAsync(new courseInstructorEditPage(ci));
        }

        private async void DeleteCI_Clicked(object sender, EventArgs e)
        {
            if(ci.courseInstructorId == 1)
            {
                await DisplayAlert("Permission Denied", "You can not remove the unassigned mentor course instructor entry.", "Okay");
                return;
            }

            bool response = await DisplayAlert("Confirm Removal", "Are you sure you want to remove this course instructor", "Yes", "No");
            if (response)
            {
                await hasCourses();
                if (valid == false)
                {
                    return;
                }

                if (valid == true)
                {
                    using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
                    {
                        //context.CreateTable<Ci>();
                        int rows = context.Delete(ci);
                        if (rows > 0) { await DisplayAlert("Sucess", "The course instructor was removed", "Okay"); }
                        else { await DisplayAlert("Failure", "The course instructor failed to be deleted", "Okay"); }
                        await Navigation.PopAsync();
                    }
                    
                }
            }
        }
        private async Task hasCourses()
        {
            List<classTable> ciHasCoursesList = classTable.GetClasses().Where(n=> n.courseInstructorId == ci.courseInstructorId).ToList();
            if(ciHasCoursesList.Count > 0)
            {
                string alertMsg = "This course instructor is associated with " + ciHasCoursesList.Count + " class(es). Removing will set these course's ci to being unassigned.";
                bool coursesResponse= await DisplayAlert("Confirm Removal", alertMsg, "Yes", "No");
                if(coursesResponse == false)
                {
                    valid = false;
                    return;
                }
                if(coursesResponse == true)
                {
                    valid = true;
                    for (int i = 0; i < ciHasCoursesList.Count; i++)
                    {
                        ciHasCoursesList[i].courseInstructorId = 1;
                        ciHasCoursesList[i].CiName = "Unassigned Course Instructor";
                            using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
                        {
                            context.Update(ciHasCoursesList[i]);
                        }

                    }
                }
            }
            else { valid = true; }
        }

        private bool valid = false;
        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    MessagingCenter.Unsubscribe<Ci>(this, "Update");
        //}
    }
}