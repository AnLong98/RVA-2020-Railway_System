using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using Common.DomainModels;

namespace Client.Commands.Road_commands
{
    public class AttachStationsToRoadCommand : ICommand
    {
        private RoadInputViewModel reciever;

        public AttachStationsToRoadCommand(RoadInputViewModel reciever)
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
            reciever.AddStationsToRoad(parameter as Station);
        }
    }
}
