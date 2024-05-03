using Application.Interfaces;
using Common.Interfaces;
using Domain.Entities;
using Newtonsoft.Json;

namespace Application.Services
{
    internal class LogService : ILogService
    {
        private ILoggerIuow _uow;

        public LogService(ILoggerIuow uow)
        {
            _uow = uow;
        }

        public async Task Save(object message, string categoryName)
        {
            var logEntry = new LogEntry
            {
                ServiceName = categoryName,
                Timestamp = DateTime.UtcNow,
                Message = JsonConvert.SerializeObject(message)
            };

            var context = _uow.GetRepository<LogEntry>();

            await context.CreateAsync(logEntry);

            await _uow.SaveChangesAsync();
        }
    }
}
