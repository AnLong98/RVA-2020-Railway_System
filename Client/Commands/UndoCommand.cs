using Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Commands
{
    public class UndoCommand : ICommand
    {
        private IPrimaryEntityCommandManagement reciever;

        public UndoCommand(IPrimaryEntityCommandManagement reciever)
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
            return reciever.CanUndo();
        }

        public void Execute(object parameter)
        {
            reciever.Undo();
        }
    }
}
