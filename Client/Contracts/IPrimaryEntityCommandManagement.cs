using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Contracts
{
    public interface IPrimaryEntityCommandManagement
    {
        bool CanUndo();
        bool CanRedo();
        void Undo();
        void Redo();
        void Add(IPrimaryEntityCommand command);
    }
}
