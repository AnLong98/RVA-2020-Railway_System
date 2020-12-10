using Server.Contracts;
using Server.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Factories
{
    public class DatabaseContextFactory : IDatabaseContextFactory
    {
        public RailwayContext GetContext()
        {
            return new RailwayContext();
        }
    }
}
