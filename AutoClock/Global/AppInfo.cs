using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using AutoClock.Interface;

namespace AutoClock.Global
{
    internal class AppInfo
    {
        static internal string Path = AppDomain.CurrentDomain.BaseDirectory;
        static internal List<Nefe.PluginCore.Plugin> Plugins = new();
        static internal List<IClocker> Clockers = new();
        static internal List<IMonitor> Monitors = new();
        
        static internal JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkCompatibilityIdeographs, UnicodeRanges.CjkSymbolsandPunctuation),
            IgnoreReadOnlyProperties = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
        };
    }
}