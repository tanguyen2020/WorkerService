using System;
using System.Collections.Generic;
using System.Text;

namespace FPTeLabService
{
    public static class ConsoleWriter
    {
        private static object _MessageLock = new object();

        public static void WriteMessage(string message, ConsoleColor consoleColor)
        {
            lock (_MessageLock)
            {
                Console.ForegroundColor = consoleColor;
                Console.WriteLine(message, consoleColor);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
