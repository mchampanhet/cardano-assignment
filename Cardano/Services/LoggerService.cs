namespace Cardano.Services
{
    internal static class LoggerService
    {
        internal static void LogInfo(string message)
        {
            Console.WriteLine(message);
        }

        internal static void LogWarning(string message)
        {
            Console.WriteLine("Warning: " + message);
        }

        internal static void LogError(string message, Exception? ex = null)
        {
            Console.WriteLine("Error: " + message);
            if (ex != null)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
