using Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Managers
{
    public class PrimaryEntityCommandManager : IPrimaryEntityCommandManagement
    {
        private Stack<IPrimaryEntityCommand> UndoCommands;
        private Stack<IPrimaryEntityCommand> RedoCommands;

        public PrimaryEntityCommandManager()
        {
            UndoCommands = new Stack<IPrimaryEntityCommand>();
            RedoCommands = new Stack<IPrimaryEntityCommand>();
        }

        public void Add(IPrimaryEntityCommand command)
        {
            UndoCommands.Push(command);
        }

        public void Redo()
        {
            if (CanRedo())
            {
                IPrimaryEntityCommand command = RedoCommands.Pop();
                command.Redo();
                UndoCommands.Push(command);
            }
        }

        public void Undo()
        {
            if (CanUndo())
            {
                IPrimaryEntityCommand command = UndoCommands.Pop();
                command.Undo();
                RedoCommands.Push(command);
            }
        }

        public bool CanUndo()
        {
            return UndoCommands.Count != 0;
        }

        public bool CanRedo()
        {
            return RedoCommands.Count != 0;
        }
    }
}
