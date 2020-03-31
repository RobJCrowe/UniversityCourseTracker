using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Crowe_Robert.Model
{
    [SQLite.Table("table_NotifyTable")]
    public class notifyTable
    {
        [PrimaryKey]
        public int settingsID { get; set; }

        public bool notifyCourseStart { get; set; }
        public bool notifyCourseEnd { get; set; }
        public bool notifyAnticipatedAssessment { get; set; }
        public bool notifyAssessmentEnd { get; set; } 

        public bool HasRunBefore { get; set; }

        public static notifyTable GetNotifyTable()
        {
            try
            {
                using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
                {
                    //notifyTable notifyTables = context.Table<notifyTable> ().FirstOrDefault(n => n.settingsID == 1);
                    notifyTable notifyTables = context.Get<notifyTable>(1);

                    return notifyTables;
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}
