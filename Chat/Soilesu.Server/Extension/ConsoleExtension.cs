using System;
using System.Collections.Generic;
using System.Text;

namespace ManualHttpServer.Extensions
{
    public static class ConsoleExtension
    {
        private static readonly ConsoleColor _defaultForegroundColor = Console.ForegroundColor;
        public static void WriteLine(string text, 
            ConsoleColor foregroundColor)
        {
            var currentMoment = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss");
            var template = $"[{currentMoment}] {text}";

            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(template);
            Console.ForegroundColor = _defaultForegroundColor;
        }
    }
}
