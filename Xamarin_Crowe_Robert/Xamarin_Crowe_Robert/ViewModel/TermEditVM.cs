using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin_Crowe_Robert.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Crowe_Robert.ViewModel
{
    class TermEditVM : INotifyPropertyChanged
    {
        private termTable tt;
        private termTable ttOriginalValues;
        public TermEditVM() { }
        public TermEditVM(termTable TT)
        {
            tt = TT; ttOriginalValues = TT;
            TermName = TT.termName;
            StartTerm = TT.termStart;
            EndTerm = TT.termEnd;
        }

        private string termName;
        public string TermName
        {
            get { return termName; }
            set { termName = value;
                OnPropertyChanged("TermName");
            }
        }
        private DateTime startTerm;
        public DateTime StartTerm
        {
            get { return startTerm; }
            set { startTerm = value;
                OnPropertyChanged("StartTerm");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        private DateTime endTerm;
        public DateTime EndTerm
        {
            get { return endTerm; }
            set { endTerm = value;
                OnPropertyChanged("EndTerm");
            }
        }
        private bool IsTermValid()
        {
            if (string.IsNullOrEmpty(TermName))
            {
                SendErrorMsg("1"); return false;
            }
            if (StartTerm == EndTerm)
            {
                SendErrorMsg("2"); return false;
            }
            if (StartTerm.CompareTo(EndTerm) > 0)
            {
                SendErrorMsg("3"); return false;
            }
            return true;
        }

        private void SendErrorMsg(string name)
        {
            message = name;
            //MessagingCenter.Send<string>(name, "ErrorMsg");
        }
        public string message { get; set; } = "-1";

        public void UpdateTerm()
        {
            bool result = IsTermValid();
            if (result == false)
            {
                return;
            }
            if (tt.termName != TermName || tt.termStart != StartTerm || tt.termEnd != EndTerm)
            {
                tt.termName = TermName; tt.termStart = StartTerm; tt.termEnd = EndTerm;
                using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
                {
                    context.CreateTable<Ci>();
                    int rows = context.Update(tt);
                    if (rows > 0) { message = "5"; }
                    else { message = "4"; }

                }
                //MessagingCenter.Send<termTable>(tt, "Update");
                //MessagingCenter.Send<string>("back", "GoBack");
            }
        }

    }
}
