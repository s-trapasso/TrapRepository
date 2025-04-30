using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneVeicoli.Log
{
    public interface ILoggingService
    {
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message, Exception? ex = null);
        void Fatal(string message, Exception? ex = null);
    }
}
