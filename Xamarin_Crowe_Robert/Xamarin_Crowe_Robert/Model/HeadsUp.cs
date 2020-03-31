using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin_Crowe_Robert.Model;

namespace Xamarin_Crowe_Robert.Model
{
    class HeadsUp
    {
        notifyTable nt;
        List<classTable> ct;
        //List<classTable> notificationList;
        string todaysDate;

        public HeadsUp()
        {
                        
        }

        public void SetProps()
        {
            DateTime todayUTC = DateTime.UtcNow;
            TimeZoneInfo tz = TimeZoneInfo.Local;
            TimeSpan baseoffset = tz.GetUtcOffset(todayUTC);
            DateTime today = todayUTC.Add(baseoffset);
            todaysDate = today.ToString("yyyy-MM-dd");

            nt = notifyTable.GetNotifyTable();
            ct = classTable.GetClasses();
        }

        public void RunAllNotifications()
        {
            NotifyCourseStart();
            NotifyCourseEnd();
            NotifyAssessmentStart();
            NotifyAssessmentEnd();
        }

        private void SendNotification(string title, string message)
        {
            CrossLocalNotifications.Current.Show(title, message);
        }

        private void NotifyCourseStart()
        {
            if (nt.notifyCourseStart == true)
            {
                for (int i = 0; i < ct.Count; i++)
                {
                    if(ct[i].courseStart.ToString("yyyy-MM-dd") == todaysDate)
                    {
                        SendNotification("Course Starting Alert", ct[i].courseName + " starts today");
                    }
                }
            }
        }

        private void NotifyCourseEnd()
        {
            if(nt.notifyCourseEnd == true)
            {
                for (int i = 0; i < ct.Count; i++)
                {
                    if (ct[i].courseEnd.ToString("yyyy-MM-dd") == todaysDate)
                    {
                        SendNotification("Course Ending Alert", ct[i].courseName + " ends today");
                    }
                }
            }
        }

        private void NotifyAssessmentStart()
        {
            if(nt.notifyAnticipatedAssessment == true)
            {
                for (int i = 0; i < ct.Count; i++)
                {
                    if (ct[i].HasOA == true)
                    {
                        if (ct[i].oaAnticipatedDueDate.ToString("yyyy-MM-dd") == todaysDate)
                        {
                            SendNotification("Objective Assessment Alert", ct[i].oaName + " starts today");
                        }
                    }
                    if(ct[i].HasPa == true)
                    {
                        if (ct[i].paAnticipatedDueDate.ToString("yyyy-MM-dd") == todaysDate)
                        {
                            SendNotification("Performance Assessment Alert", ct[i].paName + " starts today");
                        }
                    }
                }
            }
        }

        private void NotifyAssessmentEnd()
        {
            if(nt.notifyAssessmentEnd == true)
            {
                for (int i = 0; i < ct.Count; i++)
                {
                    if (ct[i].HasOA == true)
                    {
                        if (ct[i].oaEnd.ToString("yyyy-MM-dd") == todaysDate)
                        {
                            SendNotification("Objective Assessment Alert", ct[i].oaName + " ends today");
                        }
                    }
                    if (ct[i].HasPa == true)
                    {
                        if (ct[i].paEnd.ToString("yyyy-MM-dd") == todaysDate)
                        {
                            SendNotification("Performance Assessment Alert", ct[i].paName + " ends today");
                        }
                    }
                }
            }
        }
        
    }
}
