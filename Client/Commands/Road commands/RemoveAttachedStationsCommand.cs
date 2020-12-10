using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Commands.Road_commands
{
    public class RemoveAttachedStationsCommand : ICommand
    {
        private RoadInputViewModel reciever;

        public RemoveAttachedStationsCommand(RoadInputViewModel reciever)
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
                CommandManager.RequerySuggested -= value;
            }
        }


        public bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        public void Execute(object parameter)
        {
            reciever.RemoveStationsFromRoad(parameter as Station);
        }
    }
}
