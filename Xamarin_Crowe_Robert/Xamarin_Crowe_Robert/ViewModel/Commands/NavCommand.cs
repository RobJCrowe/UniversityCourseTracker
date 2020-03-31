using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Xamarin_Crowe_Robert.ViewModel.Commands
{
    public class NavCommand : ICommand
    {
        public cIMasterVM homeVM { get; set; }
        public NavCommand(cIMasterVM HomeVM)
        {
            homeVM = HomeVM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //homeVM.Nav();
        }
    }
}
