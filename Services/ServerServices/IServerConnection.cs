using VideoMonitoring.Models;
using VideoMonitoring.ModelViewer;

namespace VideoMonitoring.Services.ServerServices
{
    public interface IServerConnection
    {
        public bool CanConnect(ServerModelViewer server);
    }
}
