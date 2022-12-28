using Microsoft.EntityFrameworkCore;
using System.Text;
using VideoMonitoring.Data;
using VideoMonitoring.Models;
using VideoMonitoring.ModelViewer;

namespace VideoMonitoring.Repositories
{
    public enum ERepositoryResponse
    {
        Ok,
        NotFount,
        Error
    }

    public class ServerRepository : IServerRepository
    {
        AppDbContext _context;
        private object context;

        public ServerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ERepositoryResponse> CreateServerAsync(ServerModelViewer model)
        {
            var server = new Server
            {
                Name = model.Name,
                IpAddress = model.Ip,
                Port = model.Port
            };

            if(server == null)
                return ERepositoryResponse.NotFount;

            try
            {
                await _context.Servers.AddAsync(server);
                await _context.SaveChangesAsync();

                return ERepositoryResponse.Ok;
            }
            catch
            {
                return ERepositoryResponse.Error;
            }
        }

        public async Task<string?> CreateVideoAsync(VideoModelViewer model, Guid serverId)
        {
            var server = await _context
                .Servers
                .FirstOrDefaultAsync(x => x.Id == serverId);

            if (server == null)
                return null;

            var video = new Video
            {
                Id = Guid.NewGuid(),
                Description = model.Description,
                Server = server,
                SizeInBytes = Encoding.ASCII.GetByteCount(model.Data),
                CreatedAt = DateTime.Now
            };

            try
            {
                await _context.Videos.AddAsync(video);
                await _context.SaveChangesAsync();

                return video.Id.ToString();
            }
            catch
            {
                return null;
            }
        }

        public async Task<ERepositoryResponse> DeleteAsync(Guid serverId)
        {
            var server = await _context
                .Servers
                .FirstOrDefaultAsync(x => x.Id == serverId);

            if (server == null)
                return ERepositoryResponse.NotFount;

            try
            {
                _context.Servers.Remove(server);
                await _context.SaveChangesAsync();
                return ERepositoryResponse.Ok;
            }
            catch
            {
                return ERepositoryResponse.Error;
            }
        }

        public async Task<ERepositoryResponse> DeleteVideoAsync(Guid serverId, Guid videoId)
        {
            var video = await _context
                .Videos
                .FirstOrDefaultAsync(v => v.Id == videoId && v.Server.Id == serverId);

            if (video == null)
                return ERepositoryResponse.NotFount;

            try
            {
                _context.Videos.Remove(video);
                await _context.SaveChangesAsync();

                return ERepositoryResponse.Ok;
            } catch
            {
                return ERepositoryResponse.Error;
            }
        }

        public async Task<List<ServerModelViewer>> GetAsync()
        {
            var serverList = await _context
                .Servers
                .AsNoTracking()
                .Select(s => new ServerModelViewer
                {
                    Id = s.Id,
                    Ip = s.IpAddress,
                    Name = s.Name,
                    Port = s.Port
                })
                .ToListAsync();

            return serverList;
        }

        public async Task<ServerModelViewer> GetByIdAsync(Guid serverId)
        {
            var server = await _context
                .Servers
                .Select(s => new ServerModelViewer
                {
                    Id = s.Id,
                    Name = s.Name,
                    Ip = s.IpAddress,
                    Port = s.Port
                })
                .FirstOrDefaultAsync(x => x.Id == serverId);

            return server;
        }

        public async Task<VideoInfoModelViewer> GetVideoByIdAsync(Guid serverId, Guid videoId)
        {
            var video = await _context
                .Videos
                .Select(v => new VideoInfoModelViewer
                {
                    Id = v.Id,
                    Description = v.Description,
                    SizeInBytes = v.SizeInBytes
                })
                .FirstOrDefaultAsync(v => v.Id == videoId);
            return video;
        }

        public async Task<List<VideoInfoModelViewer>> GetVideosAsync(Guid serverId)
        {
            var videoList = await _context
                .Servers
                .Where(s => s.Id == serverId)
                .AsNoTracking()
                .SelectMany(s =>  s.Videos
                .Select(v => new VideoInfoModelViewer
                {
                    Id = v.Id,
                    Description = v.Description,
                    SizeInBytes = v.SizeInBytes
                })
                ).ToListAsync();
                

            return videoList;
        }
    }
}
