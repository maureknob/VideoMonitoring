
using System.ComponentModel.DataAnnotations;

namespace VideoMonitoring.Models
{
    public class Server
    {
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? IpAddress { get; set; }
        [Required]
        public int Port { get; set; }
        public ICollection<Video>? Videos { get; set; }
    }
}
