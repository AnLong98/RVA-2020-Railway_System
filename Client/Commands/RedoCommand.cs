using Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Commands
{
    public class RedoCommand : ICommand
    {
        private IPrimaryEntityCommandManagement reciever;

        public RedoCommand(IPrimaryEntityCommandManagement reciever)
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
            return reciever.CanRedo();
        }

        public void Execute(object parameter)
        {
            reciever.Redo();
        }
    }
}
