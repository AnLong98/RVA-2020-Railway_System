using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Commands.Track_Commands
{
    public class UpdateTrackCommand : ICommand
    {
        private TrackInputViewModel receiver;

        public UpdateTrackCommand(TrackInputViewModel receiver)
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
            return parameter != null && (parameter as Track).IsValid();
        }

        public void Execute(object parameter)
        {
            receiver.UpdateTrack(parameter as Track);
        }
    }
}
