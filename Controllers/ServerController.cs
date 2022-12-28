using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoMonitoring.Data;
using VideoMonitoring.Models;
using VideoMonitoring.ModelViewer;
using VideoMonitoring.Repositories;
using VideoMonitoring.Services.ServerServices;
using VideoMonitoring.Services.VideoServices;


namespace VideoMonitoring.Controllers
{
    [ApiController]
    [Route("api")]
    public class ServerController : ControllerBase
    {
        IServerRepository _serverRepository;
        IServerConnection _serverConnection;
        IVideoFileHandler _videoFileHandler;

        public ServerController(
            IServerRepository serverRepository,
            IServerConnection serverConnection,
            IVideoFileHandler videoFile)
        {
            _serverRepository = serverRepository;
            _serverConnection = serverConnection;
            _videoFileHandler = videoFile;
        }

        [HttpPost]
        [Route("server")]
        public async Task<IActionResult> CreateServerAsync([FromBody] ServerModelViewer model)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var response = await _serverRepository.CreateServerAsync(model);

            if (response.ToString() == "Ok")
                return Ok();

            if (response.ToString() == "NotFound")
                return NotFound();


            return BadRequest();
        }

        [HttpGet]
        [Route("servers")]
        public async Task<IActionResult> GetAsync()
        {
            var serverList = await _serverRepository.GetAsync();
            return Ok(serverList);
        }

        [HttpGet]
        [Route("servers/{serverId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid serverId)
        {
            var server = await _serverRepository.GetByIdAsync(serverId);

            if (server == null)
                return NotFound();

            return Ok(server);
        }

        [HttpDelete]
        [Route("servers/{serverId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid serverId)
        {
            var response = await _serverRepository.DeleteAsync(serverId);

            if (response.ToString() == "NotFound")
                return NotFound();
            if (response.ToString() == "Ok")
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        [Route("servers/available/{serverId}")]
        public async Task<IActionResult> CanConnect([FromRoute] Guid serverId)
        {
            var server = await _serverRepository.GetByIdAsync(serverId);

            if (server == null)
                return NotFound();

            return Ok(_serverConnection.CanConnect(server));
        }

        [HttpPost]
        [Route("servers/{serverId}/videos")]
        public async Task<IActionResult> CreateVideoAsync(
            [FromRoute] Guid serverId, 
            [FromBody] VideoModelViewer model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _serverRepository.CreateVideoAsync(model, serverId);

            if (response == null)
                return NotFound();

            if (response != null)
            {
                model.Id = new Guid(response);
                var videoResponse = _videoFileHandler.CreateVideoFile(model, serverId);

                if (videoResponse.ToString() == "Ok")
                    return Ok();

                return BadRequest();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("servers/{serverId}/videos")]
        public async Task<IActionResult> GetVideosAsync([FromRoute] Guid serverId)
        {
            var response = await _serverRepository.GetVideosAsync(serverId);
            return Ok(response);
        }

        [HttpDelete]
        [Route("servers/{serverId}/videos/{videoId}")]
        public async Task<IActionResult> DeleteVideoAsync(
            [FromRoute] Guid serverId, 
            [FromRoute] Guid videoId)
        {
            var response = await _serverRepository.DeleteVideoAsync(serverId, videoId);

            if(response.ToString() == "Ok")
                return Ok();

            if (response.ToString() == "NotFound")
                return NotFound();

            return BadRequest();
        }

        [HttpGet]
        [Route("servers/{serverId}/videos/{videoId}")]
        public async Task<IActionResult> GetVideoByIdAsync(
            [FromRoute] Guid serverId,
            [FromRoute] Guid videoID)
        {
            var video = await _serverRepository.GetVideoByIdAsync(serverId, videoID);
            return Ok(video);
        }

        [HttpGet]
        [Route("servers/{serverId}/videos/{videoId}/binary")]
        public async Task<IActionResult> GetVideoContentAsync(
            [FromRoute] Guid serverId,
            [FromRoute] Guid videoId)
        {
            var content = _videoFileHandler.GetVideoContent(serverId, videoId);
            return Ok(content);
        }
    }
}
