namespace VideoMonitoring.Models
{
    public class Video
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? ContentPath { get; set; }
        public Server? Server { get; set; }
    }
}
