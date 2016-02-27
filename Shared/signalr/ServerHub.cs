using System;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;

namespace Shared.SignalR
{
    [Microsoft.AspNet.SignalR.Hubs.HubName("ServerHub")]
    public class ServerHub : Hub
    {
        protected static IList<string> clients = new List<string>();

        public void RegisterClient(string clientName)
        {
            if (!clients.Contains(clientName))
            {
                clients.Add(clientName);
                Console.WriteLine("Adding " + clientName);
                Console.WriteLine("Count: " + clients.Count.ToString());
            }
        }

        public void SendServer(string clientName, string msg)
        {
            Console.WriteLine("Server Msg Received: " + msg);

            Clients.All.SendClient("Server Sending Msg - " + msg);
        }
    }
}
