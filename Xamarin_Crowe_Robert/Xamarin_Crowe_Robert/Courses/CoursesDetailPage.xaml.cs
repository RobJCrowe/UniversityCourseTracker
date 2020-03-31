using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Crowe_Robert.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Crowe_Robert.ViewModel;
using System.Collections.ObjectModel;

namespace Xamarin_Crowe_Robert.Courses
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CoursesDetailPage : ContentPage
	{
        ObservableCollection<classTable> coursesOc; //= new ObservableCollection<classTable>()
        termTable tt;

        public CoursesDetailPage ()
		{
			InitializeComponent ();
		}
        public CoursesDetailPage(termTable TT)
        {
            InitializeComponent();
            App.currentPage = this;
            tt = TT;
            MessagingCenter.Subscribe<string>(this, "GoBack", (result) =>
            {
                if (result == "back") { Navigation.PopAsync(); }
            });
            //MessagingCenter.Subscribe<MasterCourseVM>(this, "VM" (vmHolder) =>
            //    {

            //});
        }

        private void AddCourse_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCoursesPage(tt));
        }

        private void CourseListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
             classTable classHolder = e.Item as classTable;
             Navigation.PushAsync(new DetailDetailCoursePage(classHolder));
        }
        private void ButtonVisibility(int counter)
        {
            if(counter < 6)
            {
                AddCourse.IsVisible = true;
                deleteCoursesToAddMore.IsVisible = false;
            }
            else if (counter == 6)
            {
                AddCourse.IsVisible = false;
                deleteCoursesToAddMore.IsVisible = true;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            coursesOc = null;
            courseListView.ItemsSource = null;
            List<classTable> classes = classTable.GetCourses(tt.termID);

            ObservableCollection<classTable> listConverter = new ObservableCollection<classTable>(classes);
            coursesOc = listConverter;
            courseListView.ItemsSource = coursesOc;
            ButtonVisibility(coursesOc.Count);
            
        }
    }
}