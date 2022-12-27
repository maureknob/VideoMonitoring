using VideoMonitoring.Models;

namespace VideoMonitoring.Services.ServerConnection
{
    public interface IServerConnection
    {
        public bool CanConnect(Server server);
    }
}
