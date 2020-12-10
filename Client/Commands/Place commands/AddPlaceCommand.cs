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
    public class AddPlaceCommand : ICommand
    {
        private PlaceInputViewModel receiver;

        public AddPlaceCommand(PlaceInputViewModel receiver)
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
            receiver.AddPlace(parameter as Place);
        }
    }
}
