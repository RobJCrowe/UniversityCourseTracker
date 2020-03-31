using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin_Crowe_Robert.Model;

namespace Xamarin_Crowe_Robert.ViewModel
{
    public class MasterCourseVM : INotifyPropertyChanged
    {
        public MasterCourseVM()
        {

        }
        public MasterCourseVM(termTable TTin)
        {
            tt = TTin; ttOriginalValues = TTin;
            termID = TTin.termID;
            CourseMinimum = TTin.termStart; CourseMaximum = TTin.termEnd;
        }
        public MasterCourseVM(classTable CT)
        {
            ct = CT; ctOriginal = CT;
            termID = CT.termID;
            ciList = Ci.GetCis();
            SetValues();
        }

        private void SetValues()
    {
            termTable tt = termTable.GetTermID(TermID);
            CourseMinimum = tt.termStart;
            CourseMaximum = tt.termEnd;
            CourseName = ct.courseName;
        CourseStart = ct.courseStart;
        CourseEnd = ct.courseEnd;
        //if (ct.courseStatus != null)
        { CourseStatus = ct.courseStatus; }
        //if (ct.optionalNotes != null)
        { OptionalNotes = ct.optionalNotes; }
        //if (ct.oaName != null)
        { OaName = ct.oaName; }
        //if (ct.oaAnticipatedDueDate != null)
        { OaAnticipatedDueDate = ct.oaAnticipatedDueDate; }
        //if (ct.oaEnd != null)
        { OaEnd = ct.oaEnd; }
        //if (ct.paName != null)
        { PaName = ct.paName; }
        //if (ct.paAnticipatedDueDate != null)
        { PaAnticipatedDueDate = ct.paAnticipatedDueDate; }
            if (ct.paEnd != null)
            { PaEnd = ct.paEnd; }
            HasPa = ct.HasPa;
            HasOA = ct.HasOA;
    }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        private classTable ct; private classTable ctOriginal;
        private termTable ttOriginalValues;
        private termTable tt;
        public termTable TT
        {
            get { return tt; }
            set { tt = value;
                OnPropertyChanged("TT");
            }
        }
        private string courseName;

        public string CourseName
        {
            get { return courseName; }
            set { courseName = value;
                OnPropertyChanged("CourseName");
            }
        }
        private DateTime courseStart;

        public DateTime CourseStart
        {
            get { return courseStart; }
            set
            {
                //if (HasAssessment())
                //{return;}

                courseStart = value;
                CourseMinimumSet = true;
                TestMinimum = CourseStart;
                OnPropertyChanged("CourseStart");
            }
        }
        
        private DateTime courseEnd;

        public DateTime CourseEnd
        {
            get { return courseEnd; }
            set
            {
                //if (HasAssessment())
                //{ return; }
                courseEnd = value;
                CourseMaximumSet = true;
                TestMaximum = CourseEnd;
                OnPropertyChanged("CourseEnd");
            }
        }
        public bool HasAssessment()
        {
            if ((HasOA || HasPa))
            {
                sendMessage("12");
                return true;
            }
            return false;
        }
        private string courseStatus;

        public string CourseStatus
        {
            get { return courseStatus; }
            set { courseStatus = value;
                OnPropertyChanged("CourseStatus");
            }
        }
        private string optionalNotes;

        public string OptionalNotes
        {
            get { return optionalNotes; }
            set { optionalNotes = value;
                OnPropertyChanged("OptionalNotes");
            }
        }
        private int testCount;

        public int TestCount
        {
            get { return testCount; }
            set
            {
                testCount = value;
                OnPropertyChanged("TestCount");
            }
        }
        // DATE BOUNDS
        private DateTime courseMinimum;

        public DateTime CourseMinimum
        {
            get { return courseMinimum; }
            set { courseMinimum = value;
                OnPropertyChanged("CourseMinimum");
            }
        }

        private DateTime courseMaximum;

        public DateTime CourseMaximum
        {
            get { return courseMaximum; }
            set { courseMaximum = value;
                OnPropertyChanged("CourseMaximum");
            }
        }

        private DateTime testMinimum;

        public DateTime TestMinimum
        {
            get { return testMinimum; }
            set { testMinimum = value;
                OnPropertyChanged("TestMinimum");
            }
        }

        private DateTime testMaximum;

        public DateTime TestMaximum
        {
            get { return testMaximum; }
            set { testMaximum = value;
                OnPropertyChanged("TestMaximum");
            }
        }

        private bool courseMinimumSet;

        public bool CourseMinimumSet
        {
            get { return courseMinimumSet; }
            set { courseMinimumSet = value; }
        }
        private bool courseMaximumSet;

        public bool CourseMaximumSet
        {
            get { return courseMaximumSet; }
            set { courseMaximumSet = value; }
        }

        private bool testMinimumSet;
        public bool TestMinimumSet
        {
            get { return testMinimumSet; }
            set { testMinimumSet = value; }
        }
        private bool testMaximumSet;

        public bool TestMaximumSet
        {
            get { return testMaximumSet; }
            set { testMaximumSet = value; }
        }

        public bool IsCourseStartEndSet()
        {
            if (CourseStart == timeMin)
            { sendMessage("3"); return false; }
            if (CourseEnd == timeMin)
            { sendMessage("4"); return false; }
            if (CourseStart == CourseEnd)
            { sendMessage("5"); return false; }
            if (CourseStart.CompareTo(CourseEnd) > 0)
            { sendMessage("6"); return false; }
            if (CourseMinimumSet == false)
            {
                sendMessage("10");
                return false;
            }
            if (courseMaximumSet == false)
            {
                sendMessage("11");
                return false;
            }
            
            return true;
        }

        //OA
        private string oaName;

        public string OaName
        {
            get { return oaName; }
            set { oaName = value;
                OnPropertyChanged("OaName");
            }
        }

        private DateTime oaAnticipatedDueDate;

        public DateTime OaAnticipatedDueDate
        {
            get { return oaAnticipatedDueDate; }
            set { oaAnticipatedDueDate = value;
                OnPropertyChanged("OaAnticipatedDueDate");
            }
        }

        private DateTime oaEnd;

        public DateTime OaEnd
        {
            get { return oaEnd; }
            set { oaEnd = value;
                OnPropertyChanged("OaEnd");
            }
        }

        //PA

        private string paName;

        public string PaName
        {
            get { return paName; }
            set
            {
                paName = value;
                OnPropertyChanged("PaName");
            }
        }

        private DateTime paAnticipatedDueDate;

        public DateTime PaAnticipatedDueDate
        {
            get { return paAnticipatedDueDate; }
            set
            {
                paAnticipatedDueDate = value;
                OnPropertyChanged("PaAnticipatedDueDate");
            }
        }

        private DateTime paEnd;

        public DateTime PaEnd
        {
            get { return paEnd; }
            set
            {
                paEnd = value;
                OnPropertyChanged("PaEnd");
            }
        }
        //OA-PA TEMP VALUES
        public string PaNameH { get; set; }
        public DateTime PaAntH { get; set; }
        public DateTime PaEndH { get; set; }
        public string OaNameH { get; set; }
        public DateTime OaAntH { get; set; }
        public DateTime OaEndH { get; set; }
        public DateTime timeMin { get; set; } = new DateTime(2019, 1, 1);

        //FOREIGN KEYS
        private int courseInstructorId;

        public int CourseInstructorId
        {
            get { return courseInstructorId; }
            set { courseInstructorId = value;
                OnPropertyChanged("CourseInstructorId");
            }
        }

        private int termID;

        public int TermID
        {
            get { return termID; }
            
        }
        private bool hasOa;
        public bool HasOA
        {
            get { return hasOa; }
            set { hasOa = value;
                OnPropertyChanged("HasOA");
            }
        }

        private bool hasPa;

        public bool HasPa
        {
            get { return hasPa; }
            set { hasPa = value;
                OnPropertyChanged("HasPA");
            }
        }
        private bool isValid()
        {
            if (string.IsNullOrEmpty(CourseName))
            { sendMessage("1"); return false; }
            if (string.IsNullOrEmpty(CourseStatus))
            { sendMessage("2"); return false;}
            if (CourseStart == timeMin)
            { sendMessage("3"); return false;}
            if (CourseEnd == timeMin)
            { sendMessage("4"); return false;}
            if (CourseStart == CourseEnd)
            { sendMessage("5"); return false;}
            if (CourseStart.CompareTo(CourseEnd) > 0)
            {sendMessage("6"); return false;}
            if(selectedTeacherIndex == -1)
            { sendMessage("7"); return false;}
            return true;
        }

        public string validationMessage { get; set; } = "-1";
        void sendMessage(string index)
        {
            validationMessage = index;
            //MessagingCenter.Send<MasterCourseVM, string>(this, "ValidationError", index);
        }

        public void  SaveTerm()
        {
            bool result = isValid();
            if (result == false) { return; }
            classTable ctNew = new classTable();
            ctNew.courseName = CourseName;
            ctNew.courseStart = CourseStart;
            ctNew.courseEnd = CourseEnd;
            ctNew.optionalNotes = OptionalNotes;
            ctNew.courseStatus = CourseStatus;
            ctNew.termID = TT.termID;
            if(HasOA == true)
            {
                ctNew.oaName = OaName;
                ctNew.oaAnticipatedDueDate = OaAnticipatedDueDate;
                ctNew.oaEnd = OaEnd;
                ctNew.HasOA = HasOA;
            }
            if (HasPa == true)
            {
                ctNew.paName = PaName;
                ctNew.paAnticipatedDueDate = PaAnticipatedDueDate;
                ctNew.paEnd = PaEnd;
                ctNew.HasPa = HasPa;
            }
            ctNew.courseInstructorId = selectedTeacherIndex;
            ctNew.CiName = selectedTeacher.ciName;

            using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
            {
                context.CreateTable<classTable>();
                int rows = context.Insert(ctNew);
                context.Close();

                if (rows > 0)
                { sendMessage("8");}
                else
                {sendMessage("9");}
                
            }
        }

        private List<Ci> ciList;
        public List<Ci> CiList
        { get {
                return ciList;
            }
            set {
                ciList = value;
                OnPropertyChanged("CiList");
            } }

        private Ci selectedTeacher;

            public Ci SelectedTeacher
            {
                get { return selectedTeacher; }
                set { selectedTeacher = value;
                selectedTeacherIndex = selectedTeacher.courseInstructorId;
                OnPropertyChanged("SelectedTeacher");
            }
            }
        public int selectedTeacherIndex {
            get;
            set;
        } = -1;
        
        
        }
    }



