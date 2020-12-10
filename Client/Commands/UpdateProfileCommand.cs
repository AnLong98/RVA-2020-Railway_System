using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Commands
{
    public class UpdateProfileCommand : ICommand
    {
        private MainViewModel receiver;

        public UpdateProfileCommand(MainViewModel receiver)
        {
            this.receiver = receiver;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return receiver.User != null;
        }

        public void Execute(object parameter)
        {
            receiver.UpdateUser();
        }
    }
}
