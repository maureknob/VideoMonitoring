using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoMonitoring.Data;
using VideoMonitoring.Models;

namespace VideoMonitoring.Controllers
{
    [ApiController]
    [Route("api")]
    public class ServerController : ControllerBase
    {
        [HttpPost]
        [Route("server")]
        public async Task<IActionResult> CreateServerAsync(
            [FromBody] Server server, 
            [FromServices] AppDbContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            try
            {
                await context.Servers.AddAsync(server);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("servers")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context)
        {
            var servers = await context
                .Servers
                .AsNoTracking()
                .ToListAsync();
            
            return Ok(servers);
        }

        [HttpGet]
        [Route("servers/{serverId}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] Guid serverId,
            [FromServices] AppDbContext context)
        {
            var server = await context
                .Servers
                .FirstOrDefaultAsync(x => x.Id == serverId);

            if (server == null)
                return NotFound();

            return Ok(server);
        }

        [HttpDelete]
        [Route("servers/{serverId}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid serverId,
            [FromServices] AppDbContext context)
        {
            var server = await context
                .Servers
                .FirstOrDefaultAsync(x => x.Id == serverId);

            if (server == null)
                return NotFound();

            try
            {
                context.Servers.Remove(server);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
