using VideoMonitoring.Models;
using VideoMonitoring.ModelViewer;
using static VideoMonitoring.Services.VideoServices.VideoFileHandler;

namespace VideoMonitoring.Services.VideoServices
{
    public interface IVideoFileHandler
    {
        public EVideoFileHandlerResponse CreateVideoFile(VideoModelViewer modle, Guid serverId);
        public bool DeleteVideoFile(Video video);
        public string GetVideoContent(Guid ServerId, Guid videoId);
    }
}
