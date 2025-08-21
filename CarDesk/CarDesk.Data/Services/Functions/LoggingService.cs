using CarDesk.Data.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarDesk.Data.Services.Functions
{
    public class LoggingService<T> : ILoggingService<T>
    {
        private readonly ILogger<T> _logger;

        public LoggingService(ILogger<T> logger) => _logger = logger;

        public void LogInformation(string message, [CallerFilePath] string filePath = "", params object[] args)
        {
            var pageName = Path.GetFileNameWithoutExtension(filePath);
            _logger.LogInformation($"[{pageName}] {message}", args);
        }

        public void LogWarning(string message, [CallerFilePath] string filePath = "", params object[] args)
        {
            var pageName = Path.GetFileNameWithoutExtension(filePath);
            _logger.LogWarning($"[{pageName}] {message}", args);
        }

        public void LogError(Exception ex, string message, [CallerFilePath] string filePath = "", params object[] args)
        {
            var pageName = Path.GetFileNameWithoutExtension(filePath);
            _logger.LogError(ex, $"[{pageName}] {message}", args);
        }
    }
}
