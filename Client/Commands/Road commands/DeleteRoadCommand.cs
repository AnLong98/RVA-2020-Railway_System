using Client.Contracts;
using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

namespace Client.Commands
{
    public class DeleteRoadCommand : IPrimaryEntityCommand
    {
        public MainViewModel Reciever { get; set; }

        public DeleteRoadCommand(MainViewModel receiver, IPrimaryEntityCommandManagement commandManager)
        {
            Reciever = receiver;
            PrimaryEntityCommandManager = commandManager;
        }

        public DeleteRoadCommand(DeleteRoadCommand command)
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
            Reciever.DeleteSelectedRoad(PredicatePreviousState);
        }

        public void Execute(object parameter)
        {
            Road road = parameter as Road;
            PredicatePreviousState = road.DeepCopy();
            PredicatePostState = road.DeepCopy();
            PredicatePostState.Id = 0;
            Reciever.DeleteSelectedRoad(road);
            PrimaryEntityCommandManager.Add(new DeleteRoadCommand(this));

        }

        public void Undo()
        {
            PredicatePreviousState =  Reciever.AddRoad(PredicatePostState);
        }
    }
}
