using Client.Contracts;
using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Managers
{
    public class ClientEventManager
    {
        private static ClientEventManager instance;
        private List<ClientEvent> events; 
        private List<IClientEventNotifiable> observers;

        private ClientEventManager()
        {
            events = new List<ClientEvent>();
            observers = new List<IClientEventNotifiable>();

        }

        public static ClientEventManager GetOrCreate()
        {
            if (instance == null) instance = new ClientEventManager();
            return instance;
        }

        public void AddEvent(ClientEvent clientEvent)
        {
            events.Add(clientEvent);
            NotifyAll();
        }

        public void RegisterEventObserver(IClientEventNotifiable observer)
        {
            if (observers.Contains(observer)) return;
            observers.Add(observer);
        }

        public void UnregsiterEventObserver(IClientEventNotifiable observer)
        {
            if (!observers.Contains(observer)) return;
            observers.Remove(observer);
        }

        private void NotifyAll()
        {
            foreach(IClientEventNotifiable client in observers)
            {
                client.NotifyEventsUpdated(events);
            }
        }


    }
}
