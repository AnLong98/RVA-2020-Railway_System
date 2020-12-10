using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Commands
{
    public class RefreshRoadsCommand : ICommand
    {
        private MainViewModel receiver;

        public RefreshRoadsCommand(MainViewModel receiver)
        {
            this.receiver = receiver;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            receiver.RefreshRoadsList();
        }
    }
}
