using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Commands.Place_commands
{
    public class UpdatePlaceCommand : ICommand
    {
        private PlaceInputViewModel receiver;

        public UpdatePlaceCommand(PlaceInputViewModel receiver)
        {
            this.receiver = receiver;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return parameter != null && (parameter as Place).IsValid();
        }

        public void Execute(object parameter)
        {
            receiver.UpdatePlace(parameter as Place);
        }
    }
}
