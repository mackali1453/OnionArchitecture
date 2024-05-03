namespace Github.NetCoreWebApp.Infrastructure.Common.Models
{
    public class LogSettings
    {
        public LogLevelSettings LogLevel { get; set; }
    }

    public class LogLevelSettings
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
        public string LogLevel { get; set; }
    }
}
