using Server.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;



namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerInitializer initializer = new ServerInitializer();
            initializer.StartServer();
            Console.ReadKey();
        }
    }
}
