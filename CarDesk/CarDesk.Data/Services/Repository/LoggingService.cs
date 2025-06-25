using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDesk.Data.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace CarDesk.Data.Services.Repository
{
    public class LoggingService<T> : ILoggingService<T>
    {
        private readonly ILogger<T> _logger;
        public LoggingService(ILogger<T> logger) => _logger = logger;

        public void LogInformation(string message, params object[] args) =>
            _logger.LogInformation(message, args);

        public void LogWarning(string message, params object[] args) =>
            _logger.LogWarning(message, args);

        public void LogError(Exception ex, string message, params object[] args) =>
            _logger.LogError(ex, message, args);
    }
}
