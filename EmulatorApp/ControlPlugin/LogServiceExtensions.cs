using System.Diagnostics;
using Emulator.Shell.Interfaces;

namespace Emulator.ControlPlugin
{
    internal static class LogServiceExtensions
    {
        public static string Prefix => "ControlPlugin (" + Process.GetCurrentProcess().Id + "): ";

        public static void Message(this ILogService logService, string text, bool usePrefix)
        {
            logService.Message(usePrefix ? Prefix + text : text);
        }

        public static void Error(this ILogService logService, string text, bool usePrefix)
        {
            logService.Error(usePrefix ? Prefix + text : text);
        }
    }
}
