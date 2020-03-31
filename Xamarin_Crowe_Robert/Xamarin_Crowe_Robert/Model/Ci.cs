using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Crowe_Robert.Model
{
    public class Ci// : INotifyPropertyChanged
    {
        
        private int _courseInstructorId;
        [PrimaryKey, AutoIncrement]
        public int courseInstructorId
        {
            get { return _courseInstructorId; }
            set { _courseInstructorId = value; }
        }

        private string _ciName;
        [MaxLength(50)]
        public string ciName
        {
            get { return _ciName; }
            set { _ciName = value; /*OnPropertyChanged("ciName");*/ }
        }

        private string _ciEmail;
        [MaxLength(50)]
        public string ciEmail
        {
            get { return _ciEmail; }
            set { _ciEmail = value; /*OnPropertyChanged("ciEmail");*/ }
        }

        private string _ciPhone;
        [MaxLength(20)]
        public string ciPhone
        {
            get { return _ciPhone; }
            set { _ciPhone = value;
                /*OnPropertyChanged("ciPhone");*/ }
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged(string propertyName)
        //{
        //    if(PropertyChanged != null)
        //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}

        public static List<Ci> GetCis()
        {
            try
            {
                using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
                {
                    context.CreateTable<Ci>();
                    List<Ci> cis = context.Table<Ci>().ToList();
                    return cis;
                }
            }
            catch(Exception ex)
            {
                return null;
                
            }
        }
        public static Ci GetCiID(int index)
        {
            try
            {
                using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
                {
                    Ci ci = context.Get<Ci>(index);
                    return ci;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
