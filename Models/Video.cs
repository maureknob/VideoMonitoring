namespace VideoMonitoring.Models
{
    public class Video
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public byte[]? Content { get; set; }
    }
}
