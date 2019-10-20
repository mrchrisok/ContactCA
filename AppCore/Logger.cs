using System.Diagnostics;

namespace AppCore
{
   public interface ILogger
   {
      void Log(string message);
   }

   public class Logger : ILogger
   {
      public void Log(string message)
      {
         // what kind of logging to do?

         Trace.WriteLine(message);
      }
   }
}