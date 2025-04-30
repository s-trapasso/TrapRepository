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
    }

    public class LoggingServiceFactory : ILoggingServiceFactory
    {
        public ILoggingService CreateLogger<T>() => new LoggingService(typeof(T));
    }

}
