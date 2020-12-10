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
    public class UpdateStationCommand : ICommand
    {
        private StationInputViewModel receiver;

        public UpdateStationCommand(StationInputViewModel receiver)
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
            receiver.UpdateStation(parameter as Station);
        }
    }
}
