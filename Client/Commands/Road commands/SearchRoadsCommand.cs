using Client.Models;
using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Commands.Road_commands
{
    public class SearchRoadsCommand : ICommand
    {
        private MainViewModel receiver;

        public SearchRoadsCommand(MainViewModel receiver)
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
            return parameter != null && (parameter as RoadSearchModel).IsValid();
        }

        public void Execute(object parameter)
        {
            receiver.SearchRoads(parameter as RoadSearchModel);
        }
    }
}
