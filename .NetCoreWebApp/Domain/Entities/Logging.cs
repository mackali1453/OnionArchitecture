namespace Github.NetCoreWebApp.Core.Domain.Entities
{
    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
        public string LogLevel { get; set; }
    }

}
