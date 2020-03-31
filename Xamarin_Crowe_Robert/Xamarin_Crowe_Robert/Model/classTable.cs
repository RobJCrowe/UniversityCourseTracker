using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin_Crowe_Robert.Model
{
    public class classTable
    {
        [PrimaryKey, AutoIncrement]
        public int classID { get; set; }

        public int courseInstructorId { get; set; }
        public string CiName { get; set; }
        
        public int termID { get; set; }

        [MaxLength(50)]
        public string courseName { get; set; }
        public DateTime courseStart { get; set; }
        public DateTime courseEnd { get; set; }
        [MaxLength(50)]
        public string courseStatus { get; set; }
        [MaxLength(250)]
        public string optionalNotes { get; set; }
        public int testCount { get; set; }

        [MaxLength(50)]
        public string oaName { get; set; }
        public DateTime oaAnticipatedDueDate { get; set; }
        public DateTime oaEnd { get; set; }

        [MaxLength(50)]
        public string paName { get; set; }
        public DateTime paAnticipatedDueDate { get; set; }
        public DateTime paEnd { get; set; }

        private bool hasOa;

        public bool HasOA
        {
            get { return hasOa; }
            set { hasOa = value; }
        }

        private bool hasPa;

        public bool HasPa
        {
            get { return hasPa; }
            set { hasPa = value; }
        }


        //private termTable tt;
        //[Ignore]
        //public termTable TT
        //{
        //    get { return tt; }
        //    set { tt = value; }
        //}


        public static List<classTable> GetCourses(int termIndex)
        {
            try
            {
                using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
                {
                    context.CreateTable<classTable>();
                    List<classTable> classes = context.Table<classTable>().Where(n => n.termID == termIndex).ToList();
                    return classes;
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public static List<classTable> GetClasses()
        {
            try
            {
                using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
                {
                    context.CreateTable<classTable>();
                    List<classTable> classes = context.Table<classTable>().ToList();
                    return classes;
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }

    }
}
