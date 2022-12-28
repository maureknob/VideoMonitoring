using VideoMonitoring.Models;
using VideoMonitoring.ModelViewer;

namespace VideoMonitoring.Repositories
{
    public interface IServerRepository
    {
        public Task<ERepositoryResponse> CreateServerAsync(ServerModelViewer model);
        public Task<List<ServerModelViewer>> GetAsync();
        public Task<ERepositoryResponse> DeleteAsync(Guid serverId);
        public Task<ServerModelViewer> GetByIdAsync(Guid id);
        public Task<string?> CreateVideoAsync(VideoModelViewer model, Guid serverId);
        public Task<List<VideoInfoModelViewer>> GetVideosAsync(Guid serverId);
        public Task<ERepositoryResponse> DeleteVideoAsync(Guid serverId, Guid videoId);
        public Task<VideoInfoModelViewer> GetVideoByIdAsync(Guid serverId, Guid videoId);
    }
}
