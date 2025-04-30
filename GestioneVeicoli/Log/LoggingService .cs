using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace GestioneVeicoli.Log
{
    public class LoggingService : ILoggingService
    {
        private readonly ILog _log;

        public LoggingService(Type type)
        {
            _log = LogManager.GetLogger(type);
        }

        public void Debug(string message) => _log.Debug(message);
        public void Info(string message) => _log.Info(message);
        public void Warn(string message) => _log.Warn(message);
        public void Error(string message, Exception? ex = null) => _log.Error(message, ex);
        public void Fatal(string message, Exception? ex = null) => _log.Fatal(message, ex);
    }
}
