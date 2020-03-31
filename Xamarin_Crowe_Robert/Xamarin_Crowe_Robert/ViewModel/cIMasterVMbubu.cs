using System;
using System.Collections.Generic;
using System.Text;
using Xamarin_Crowe_Robert.ViewModel.Commands;
using Xamarin_Crowe_Robert.Model;
using Xamarin.Forms;
using System.ComponentModel;

namespace Xamarin_Crowe_Robert.ViewModel
{
    
    public class cIMasterVM : INotifyPropertyChanged
    {
        private Ci ci;
        public NavCommand navcmd;
        public Ci Ci
        {
            get { return ci; }
            set
            {
                ci = value;
                OnPropertyChanged("Ci");
            }
        }

        private string ciName;

        public string CiName
        {
            get { return ciName; }
            set { ciName = value;
                OnPropertyChanged("CiName");
            }
        }

        private string ciPhone;

        public string CiPhone
        {
            get { return ciPhone; }
            set { ciPhone = value;
                OnPropertyChanged("CiPhone");
            }
        }
        private string ciEmail;

        public string CiEmail
        {
            get { return ciEmail; }
            set { ciEmail = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public cIMasterVM()
        {
            ci = new Ci();
            //navcmd = new NavCommand(this);

        }

        public void Nav(Ci ci)
        {
            App.Current.MainPage.Navigation.PushAsync(new courseInstructorDetail(ci));
            
        }
        public List<Ci> UpdateCis()
        {
            var cis = Ci.GetCis();
            return cis;
        }
    }
}
