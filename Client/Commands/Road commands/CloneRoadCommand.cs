using Client.Contracts;
using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Commands
{
    public class CloneRoadCommand : IPrimaryEntityCommand
    {
        public MainViewModel Reciever { get; set; }

        public CloneRoadCommand(MainViewModel receiver, IPrimaryEntityCommandManagement commandManager)
        {
            Reciever = receiver;
            this.PrimaryEntityCommandManager = commandManager;
        }

        public CloneRoadCommand(CloneRoadCommand command)
        {
            Reciever = command.Reciever;
            PrimaryEntityCommandManager = command.PrimaryEntityCommandManager;
            PredicatePostState = command.PredicatePostState;
            PredicatePreviousState = command.PredicatePreviousState;
        }

        public Road PredicatePreviousState { get; set; }
        public Road PredicatePostState { get; set; }
        public IPrimaryEntityCommandManagement PrimaryEntityCommandManager { get; set; }

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

        public void Redo()
        {
            PredicatePostState = Reciever.CloneSelectedRoad(PredicatePreviousState);
        }

        public void Execute(object parameter)
        {
            Road road = parameter as Road;
            PredicatePreviousState = road;
            PredicatePostState = Reciever.CloneSelectedRoad(road);
            PrimaryEntityCommandManager.Add(new CloneRoadCommand(this));
        }

        public void Undo()
        {
            Reciever.DeleteSelectedRoad(PredicatePostState);
        }
    }
}
