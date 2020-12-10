using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Commands.Station_commands
{
    public class AddStationCommand : ICommand
    {
        private StationInputViewModel receiver;

        public AddStationCommand(StationInputViewModel receiver)
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
            return parameter != null && (parameter as Station).IsValid();
        }

        public void Execute(object parameter)
        {
            receiver.AddStation(parameter as Station);
        }
    }
}
