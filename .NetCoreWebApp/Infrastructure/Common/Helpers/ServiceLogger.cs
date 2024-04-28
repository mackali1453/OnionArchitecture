using Application.Interfaces;
using Domain.Entities;
using Github.NetCoreWebApp.Core.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Github.NetCoreWebApp.Infrastructure.Common.Helpers
{
    public class ServiceLogger<T> : IServiceLogger<T> where T : class
    {
        private readonly string _categoryName;
        private readonly LogLevel _minimumLogLevel;
        private ILoggerIuow _uow;
        public ServiceLogger(IOptions<Logging> appsettings, ILoggerIuow uow)
        {
            _categoryName = typeof(T).Name;
            _minimumLogLevel = Enum.Parse<LogLevel>(appsettings.Value.LogLevel.LogLevel);
            _uow = uow;
        }

        public async Task Info(object message)
        {
            if (_minimumLogLevel >= LogLevel.Information)
                await Save(message);
        }
        public async Task Error(object message)
        {
            if (_minimumLogLevel >= LogLevel.Error)
                await Save(message);
        }

        public async Task Save(object message)
        {
            var logEntry = new LogEntry
            {
                ServiceName = _categoryName,
                Timestamp = DateTime.UtcNow,
                Message = JsonConvert.SerializeObject(message)
            };

            var context = _uow.GetRepository<LogEntry>();

            await context.CreateAsync(logEntry);

            await _uow.SaveChangesAsync();
        }
    }
}