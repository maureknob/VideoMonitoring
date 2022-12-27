using System.Net;
using System.Net.Sockets;
using VideoMonitoring.Models;

namespace VideoMonitoring.Services.ServerConnection
{
    public class ServerConnection : IServerConnection
    {
        public bool CanConnect(Server server)
        {
            TcpClient client = new TcpClient(server.IpAddress, server.Port);

            client.Connect(new IPEndPoint(long.Parse(server.IpAddress), server.Port));

            return client.Connected ? true : false;
        }
    }
}
