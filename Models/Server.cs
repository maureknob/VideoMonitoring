namespace VideoMonitoring.Models
{
    public class Server
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? IpAddress { get; set; }
        public int Port { get; set; }
    }
}
