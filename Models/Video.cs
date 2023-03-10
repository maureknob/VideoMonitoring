using System.ComponentModel.DataAnnotations;

namespace VideoMonitoring.Models
{
    public class Video
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public Server? Server { get; set; }
        public long SizeInBytes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
