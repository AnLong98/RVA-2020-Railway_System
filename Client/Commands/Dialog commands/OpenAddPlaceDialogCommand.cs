using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Commands.Dialog_commands
{
    public class OpenAddPlaceDialogCommand : ICommand
    {
        private MainViewModel reciever;

        public OpenAddPlaceDialogCommand(MainViewModel reciever)
        {
            this.reciever = reciever;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested += value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            reciever.OpenAddPlaceDialog();
        }
    }
}
