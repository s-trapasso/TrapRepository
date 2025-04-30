using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneVeicoli.Log
{
    public interface ILoggingServiceFactory
    {
        ILoggingService CreateLogger<T>();
        ILoggingService CreateLogger(Type type);
    }

    public class LoggingServiceFactory : ILoggingServiceFactory
    {
        public ILoggingService CreateLogger<T>() => new LoggingService(typeof(T));
        public ILoggingService CreateLogger(Type type) => new LoggingService(type);
    }

}
