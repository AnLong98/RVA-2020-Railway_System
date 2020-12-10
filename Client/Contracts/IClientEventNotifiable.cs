using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Contracts
{
    public interface IClientEventNotifiable
    {
        void NotifyEventsUpdated(List<ClientEvent> events);
    }
}
