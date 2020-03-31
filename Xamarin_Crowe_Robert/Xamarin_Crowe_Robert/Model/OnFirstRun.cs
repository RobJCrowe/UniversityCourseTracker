using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin_Crowe_Robert.Model;

namespace Xamarin_Crowe_Robert.Model
{
    class OnFirstRun
    {
        //DATABASE DATA
        
            static Ci ci = new Ci()
            {
                ciName = "Unassigned Course Instructor",
                ciPhone = "1234567",
                ciEmail = "unassigned.CourseInstructor@wgu.edu",
                courseInstructorId = 1
            };

            static Ci ci2 = new Ci() 
            {
                ciName = "Ben Worthington",
                ciPhone = "8759865214",
                ciEmail = "President@wgu.edu",
                courseInstructorId = 2
            };

            static Ci ci3 = new Ci()
            {
                ciName = "Michael Scott",
                ciPhone = "5709411234",
                ciEmail = "dunderMifflin@wgu.edu",
                courseInstructorId = 3
            };

            static termTable tt = new termTable()
            {
                termName = "2019 Summer",
                termStart = new DateTime(2019, 5, 1),
                termEnd = new DateTime(2019, 8, 31),
                termID = 1
            };

            static classTable ct0101 = new classTable 
            {
                courseInstructorId = 2,
                CiName = "Ben Worthington",
                termID = 1,
                courseName = "Math01",
                courseStart = new DateTime(2019, 5, 5),
                courseEnd = new DateTime(2019, 8, 30),
                courseStatus = "IN PROGRESS",
                optionalNotes = "Lorem ipsum dolor sit amet, consectetur ",
                testCount = 2,
                oaName = "Math Final",
                oaAnticipatedDueDate = new DateTime(2019, 8, 4),
                oaEnd = new DateTime(2019, 8, 20),
                HasOA = true,
                paName = "Math Project",
                paAnticipatedDueDate = new DateTime(2019, 8, 5),
                paEnd = new DateTime(2019, 8, 27),
                HasPa = true
            };

            static classTable ct0102 = new classTable
            {
                courseInstructorId = 2,
                CiName = "Ben Worthington",
                termID = 1,
                courseName = "Science01",
                courseStart = new DateTime(2019, 5, 2),
                courseEnd = new DateTime(2019, 8, 19),
                courseStatus = "IN PROGRESS",
                optionalNotes = "Lorem ipsum dolor sit amet, consectetur ",
                testCount = 2,
                oaName = "Science Final",
                oaAnticipatedDueDate = new DateTime(2019, 7, 1),
                oaEnd = new DateTime(2019, 8, 18),
                HasOA = true,
                paName = "Science Project",
                paAnticipatedDueDate = new DateTime(2019, 7, 1),
                paEnd = new DateTime(2019, 8, 17),
                HasPa = true
            };

            static termTable tt1 = new termTable()
            {
                termName = "2019 Winter",
                termStart = new DateTime(2019, 9, 1),
                termEnd = new DateTime(2020, 2, 29),
                termID = 1
            };

            static classTable ct0201 = new classTable
            {
                courseInstructorId = 3,
                CiName = "Michael Scott",
                termID = 2,
                courseName = "Art01",
                courseStart = new DateTime(2019, 9, 2),
                courseEnd = new DateTime(2019, 12, 31),
                courseStatus = "IN PROGRESS",
                optionalNotes = "",
                testCount = 1,
                oaName = "Art Final",
                oaAnticipatedDueDate = new DateTime(2019, 12, 15),
                oaEnd = new DateTime(2019, 12, 25),
                HasOA = true,
                HasPa = false
            };

            static classTable ct0202 = new classTable
            {
            courseInstructorId = 3,
            CiName = "Michael Scott",
            termID = 2,
            courseName = "Social Studies01",
            courseStart = new DateTime(2019, 9, 15),
            courseEnd = new DateTime(2020, 2, 27),
            courseStatus = "IN PROGRESS",
            optionalNotes = "Lorem ipsum dolor sit amet, consectetur ",
            testCount = 2,
            oaName = "Social Studies Final",
            oaAnticipatedDueDate = new DateTime(2019, 9, 19),
            oaEnd = new DateTime(2019, 9, 20),
            HasOA = true,
            paName = "Social Studies Project",
            paAnticipatedDueDate = new DateTime(2020, 2, 5),
            paEnd = new DateTime(2020, 2, 23),
            HasPa = true
            };

        static notifyTable nt = new notifyTable
        {
            settingsID = 1,
            notifyCourseStart = true,
            notifyCourseEnd = true,
            notifyAnticipatedAssessment = true,
            notifyAssessmentEnd = true,
            HasRunBefore = true
        };

            static public void loadDummyData()
        {
            using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
            {
                context.CreateTable<notifyTable>();
                context.Insert(nt);

                context.CreateTable<Ci>();
                context.Insert(ci);
                context.Insert(ci2);
                context.Insert(ci3);

                context.CreateTable<termTable>();
                context.Insert(tt);
                context.Insert(tt1);

                context.CreateTable<classTable>();
                context.Insert(ct0101);
                context.Insert(ct0102);
                context.Insert(ct0201);
                context.Insert(ct0202);
            }
        } 
    }
}

