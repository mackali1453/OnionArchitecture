namespace Github.NetCoreWebApp.Core.Domain.Entities
{
    public class LogEntry
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public string ServiceName { get; set; }
    }
}
