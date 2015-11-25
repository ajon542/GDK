using System;

namespace Utilities
{
    /// <summary>
    /// Simple logging utility.
    /// </summary>
    public static class Log
    {
        public static void Info(string message)
        {
            Console.Write("[INFO] ");
            Console.WriteLine(message);
        }

        public static void Info(string format, params object[] args)
        {
            Console.Write("[INFO] ");
            Console.WriteLine(format, args);
        }

        public static void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[WARN] ");
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Warn(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[WARN] ");
            Console.WriteLine(format, args);
            Console.ResetColor();
        }

        public static void Fail(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[FAIL] ");
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Fail(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[FAIL] ");
            Console.WriteLine(format, args);
            Console.ResetColor();
        }
    }
}
