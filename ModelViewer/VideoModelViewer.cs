using System.ComponentModel.DataAnnotations;

namespace VideoMonitoring.ModelViewer
{
    public class VideoModelViewer
    {
        public Guid Id { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string Data { get; set; }
        public long SizeInBytes { get; set; }
    }
}
