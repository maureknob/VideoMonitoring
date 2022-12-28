using System.Net;
using System.Net.Sockets;
using VideoMonitoring.Models;
using VideoMonitoring.ModelViewer;

namespace VideoMonitoring.Services.ServerServices
{
    public class ServerConnection : IServerConnection
    {
        public bool CanConnect(ServerModelViewer server)
        {
            TcpClient client = new TcpClient(server.Ip, server.Port);

            client.Connect(new IPEndPoint(long.Parse(server.Ip), server.Port));

            return client.Connected ? true : false;
        }
    }
}
