using Server.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts
{
    public interface IDatabaseContextFactory
    {
        RailwayContext GetContext();
    }
}
