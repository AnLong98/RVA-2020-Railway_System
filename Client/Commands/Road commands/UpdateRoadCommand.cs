using Client.Contracts;
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
    public class UpdateRoadCommand : IPrimaryEntityCommand
    {
        public RoadInputViewModel Reciever { get; set; }

        public UpdateRoadCommand(RoadInputViewModel reciever, IPrimaryEntityCommandManagement primaryEntityCommandManager)
        {
            this.Reciever = reciever;
            PrimaryEntityCommandManager = primaryEntityCommandManager;
        }

        public UpdateRoadCommand(UpdateRoadCommand command)
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
            return parameter != null && (parameter as Road).IsValid();
        }

        public void Execute(object parameter)
        {
            Road road = parameter as Road;
            PredicatePreviousState = Reciever.GetRoadPreviousState(road.Id).DeepCopy();
            PredicatePostState = road.DeepCopy();
            Reciever.UpdateRoad(road);
            PrimaryEntityCommandManager.Add(new UpdateRoadCommand(this));
        }

        public void Redo()
        {
            Reciever.UpdateRoad(PredicatePostState);
        }

        public void Undo()
        {
            Reciever.UpdateRoad(PredicatePreviousState);
        }

    }
}
