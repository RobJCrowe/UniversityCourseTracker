using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin_Crowe_Robert.Model;
using Xamarin_Crowe_Robert.ViewModel;

namespace Xamarin_Crowe_Robert.ViewModel
{
    public class EDITCourseVM : INotifyPropertyChanged
    {

        public EDITCourseVM(classTable CT)
        {
            ct = CT; ctOriginal = CT;
            termID = CT.termID;
            ciList = Ci.GetCis();
            SetValues();
        }

        private void SetValues()
        {
            CourseName = ct.courseName;
            CourseStart = ct.courseStart;
            CourseEnd = ct.courseEnd;
            CourseStatus = ct.courseStatus;
            OptionalNotes = ct.optionalNotes;


            //Need To Wire Up Teacher Picker
            //SelectedTeacher = Ci.GetCiID(ct.courseInstructorId);

            
            HasPa = ct.HasPa;
            HasOA = ct.HasOA;

            if (ct.HasOA == true) //if (ct.oaName != null)
            {
                OaName = ct.oaName;
                OaAnticipatedDueDate = ct.oaAnticipatedDueDate;
                OaEnd = ct.oaEnd;
                
            }
            else
            {
                OaName = "";
                OaAnticipatedDueDate = ct.courseStart;
                OaEnd = ct.courseEnd;
            }
            if (ct.HasPa == true)
            {
                PaName = ct.paName;
                PaAnticipatedDueDate = ct.paAnticipatedDueDate;
                PaEnd = ct.paEnd;
                           }
            else
            {
                PaName = "";
                PaAnticipatedDueDate = ct.courseStart;
                PaEnd = ct.courseEnd;
            }
            termTable ttHolder = termTable.GetTermID(TermID);
            CourseMinimum = ttHolder.termStart;
            CourseMaximum = ttHolder.termEnd;

            CourseInstructorId = ct.courseInstructorId;

            Ci ciHolder = Ci.GetCiID(CourseInstructorId);
            List<Ci> ciListHolder = Ci.GetCis();

            for (int i = 0; i < ciListHolder.Count; i++)
            {
                if (ciListHolder[i].courseInstructorId == ciHolder.courseInstructorId)
                {

                    SINDEX = i;

                }
            }
            SelectedTeacher = ciHolder;
            
            TestMinimum = CourseStart;
            TestMaximum = CourseEnd;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        private classTable ct; private classTable ctOriginal;
        private termTable ttOriginalValues;
        private termTable tt;
        public termTable TT
        {
            get { return tt; }
            set
            {
                tt = value;
                OnPropertyChanged("TT");
            }
        }

        private int sIndex;

        public int SINDEX
        {
            get { return sIndex; }//sIndex; }
            set { sIndex = value;
                OnPropertyChanged("SINDEX");
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
                courseStart = value;
                CourseMinimumSet = true;
                TestMinimum = CourseStart;
                OnPropertyChanged("CourseStart");
            }
        }


        public int CourseStartIndex = 0;
        
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
        
        private string paNameH;

        public string PaNameH
        {
            get { return paNameH; }
            set { paNameH = value;
                OnPropertyChanged("PaNameH");
            }
        }

        private DateTime paAntH;

        public DateTime PaAntH
        {
            get { return paAntH; }
            set { paAntH = value;
                OnPropertyChanged("PaAntH");
            }
        }

        private DateTime paEndH;

        public DateTime PaEndH
        {
            get { return paEndH; }
            set { paEndH = value;
                OnPropertyChanged("PaEndH");
            }
        }

        private string oaNameH;

        public string OaNameH
        {
            get { return oaNameH; }
            set { oaNameH = value;
                OnPropertyChanged("OaNameH");
            }
        }

        private DateTime oaAntH;
        public DateTime OaAntH
        {
            get { return oaAntH; }
            set { oaAntH = value;
                OnPropertyChanged("OaAntH");
            }
        }

        private DateTime oaEndH;
        public DateTime OaEndH
        {
            get { return oaEndH; }
            set { oaEndH = value;
                OnPropertyChanged("OaEndH");
            }
        }

        private DateTime _timeMin;
        public DateTime timeMin
        {
            get { return _timeMin; }
            set { _timeMin = value;
                OnPropertyChanged("timeMin");
            }
        }

        //private DateTime timeMin = new DateTime(2019, 1, 1);

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

        private string validationMessage;

        public string ValidationMessage
        {
            get { return validationMessage; }
            set { validationMessage = value; }
        }

        void sendMessage(string index)
        {
            ValidationMessage = index;
            //MessagingCenter.Send<EDITCourseVM, string>(this, "ValidationError", index);
        }

        public void  SaveTerm()
        {
            bool result = isValid();
            if (result == false) { return; }
            classTable ctNew = new classTable();
            ctNew.classID = ct.classID;
            ctNew.courseName = CourseName;
            ctNew.courseStart = CourseStart;
            ctNew.courseEnd = CourseEnd;
            ctNew.optionalNotes = OptionalNotes;
            ctNew.courseStatus = CourseStatus;
            ctNew.termID = TermID;
            if(HasOA == true)
            {
                ctNew.oaName = OaName;
                ctNew.oaAnticipatedDueDate = OaAnticipatedDueDate;
                ctNew.oaEnd = OaEnd;
                ctNew.HasOA = HasOA;
            }
            else { ctNew.HasOA = HasOA; }
            if (HasPa == true)
            {
                ctNew.paName = PaName;
                ctNew.paAnticipatedDueDate = PaAnticipatedDueDate;
                ctNew.paEnd = PaEnd;
                ctNew.HasPa = HasPa;
            }
            else { ctNew.HasPa = HasPa; }
            ctNew.courseInstructorId = selectedTeacherIndex;
            ctNew.CiName = selectedTeacher.ciName;

            using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
            {
                //context.CreateTable<classTable>();
                int rows = context.Update(ctNew);
                ctNewHolder = ctNew;
                MessagingCenter.Send<EDITCourseVM, classTable>(this, "UPDATED_CT", ctNew);
                context.Close();
                if (rows > 0)
                {
                    
                    sendMessage("13");
                }
                else
                {sendMessage("14");}
                
            }
        }
        public classTable ctNewHolder { get; set; }
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



