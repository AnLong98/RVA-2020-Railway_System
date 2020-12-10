using Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Contracts
{
    public interface IPrimaryEntityCommand : ICommand
    {
        void Redo();
        void Undo();
        Road PredicatePreviousState { get; set; }
        Road PredicatePostState { get; set; }
        IPrimaryEntityCommandManagement PrimaryEntityCommandManager { get; set; }
    }
}
