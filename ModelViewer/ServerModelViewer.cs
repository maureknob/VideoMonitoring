using System.ComponentModel.DataAnnotations;

namespace VideoMonitoring.ModelViewer
{
    public class ServerModelViewer
    {
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Ip { get; set; }
        [Required]
        public int Port { get; set; }
    }
}
