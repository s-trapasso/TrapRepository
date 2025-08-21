using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarDesk.Data.Services.Interfaces
{
    public interface ILoggingService<T>
    {
        void LogInformation(string message, [CallerFilePath] string filePath = "", params object[] args);
        void LogWarning(string message, [CallerFilePath] string filePath = "", params object[] args);
        void LogError(Exception ex, string message, [CallerFilePath] string filePath = "", params object[] args);
    }
}
