using Common.Interfaces;
using Github.NetCoreWebApp.Infrastructure.Common.Interfaces;
using Github.NetCoreWebApp.Infrastructure.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Github.NetCoreWebApp.Infrastructure.Common.Helpers
{
    public class ServiceLogger<T> : IServiceLogger<T> where T : class
    {
        private readonly string _categoryName;
        private readonly LogLevel _minimumLogLevel;
        private ILogService _logService;
        public ServiceLogger(IOptions<LogSettings> logSettings, ILogService logService)
        {
            _categoryName = typeof(T).Name;
            _minimumLogLevel = Enum.Parse<LogLevel>(logSettings.Value.LogLevel.LogLevel);
            _logService = logService;
        }

        public async Task Info(object message)
        {
            if (_minimumLogLevel >= LogLevel.Information)
                await _logService.Save(message, _categoryName);
        }
        public async Task Error(object message)
        {
            if (_minimumLogLevel >= LogLevel.Error)
                await _logService.Save(message, _categoryName);
        }
    }
}