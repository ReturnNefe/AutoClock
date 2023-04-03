namespace AutoClock.Interface
{
    static public class Logger
    {
        static public void NextLine() => Console.WriteLine();
        
        static public void Log(string sender, string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"[{sender}]");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"[{DateTime.Now}] ");
            Console.ResetColor();
            
            Console.WriteLine(message);
        }
        
        static public void Log(Plugin sender, string message) => Log(sender.Info.Name, message);
    }
}