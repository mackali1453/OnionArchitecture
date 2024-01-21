using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Core.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Github.NetCoreWebApp.Infrastructure.Common.DbLogger
{
    public class ServiceLogger<T> : IServiceLogger<T> where T : class
    {
        private readonly string _categoryName;
        private readonly LogLevel _minimumLogLevel;
        private readonly IUow _iuow;
        public ServiceLogger(IOptions<Logging> appsettings, IUow uow)
        {
            _categoryName = typeof(T).Name;
            _minimumLogLevel = Enum.Parse<LogLevel>(appsettings.Value.LogLevel.LogLevel);
            _iuow = uow;
        }

        public void Info(object message)
        {
            if (_minimumLogLevel >= LogLevel.Information)
                Save(message);
        }
        public void Error(object message)
        {
            if (_minimumLogLevel >= LogLevel.Error)
                Save(message);
        }

        public void Save(object message)
        {
            var logEntry = new LogEntry
            {
                ServiceName = _categoryName,
                Timestamp = DateTime.UtcNow,
                Message = JsonConvert.SerializeObject(message)
            };

            var logRepo = _iuow.GetLogRepository<LogEntry>();
            logRepo.CreateAsync(logEntry);
        }
    }
}