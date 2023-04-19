namespace AutoClock.Interface
{
    static public class Logger
    {
        static private object locker = new();

        static public void NextLine() { lock (locker) Console.WriteLine(); }

        static public void Log(string sender, string message)
        {
            lock (locker)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"[{sender}]");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"[{DateTime.Now}] ");
                Console.ResetColor();

                Console.WriteLine(message);
            }
        }

        static public void Log(IPlugin sender, string message) => Log(sender.Info.Name, message);
    }
}