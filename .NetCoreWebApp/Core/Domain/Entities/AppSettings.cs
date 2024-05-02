namespace Domain.Entities
{
    public class LoggingSettings
    {
        public LogLevelSettings LogLevel { get; set; }
    }

    public class LogLevelSettings
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
        public string LogLevel { get; set; }
    }

    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
    }

    public class ConnectionStrings
    {
        public string Local { get; set; }
    }

    public class AppSettings
    {
        public LoggingSettings Logging { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public string AllowedHosts { get; set; }
    }
}
