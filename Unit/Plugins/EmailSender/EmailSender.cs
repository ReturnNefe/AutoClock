using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using AutoClock.Interface;

namespace EmailSender
{
    public class EmailSender : IMonitor
    {
        // Private Properties
        private PluginInfo info = new("EmailSender", "ReturnNefe", "Send email when AutoClock failed to clock-in.", new("0.1.0"));
        private Setting? setting;

        // Public Properties
        public PluginInfo Info => info;

        // Public Method
        public EmailSender() { }

        public async Task Loading(string baseDirectory)
        {
            try
            {
                // Load Config
                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkCompatibilityIdeographs, UnicodeRanges.CjkSymbolsandPunctuation),
                    IgnoreReadOnlyProperties = true,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true,
                };

                using (StreamReader reader = new(Path.Combine(baseDirectory, "config.txt"), Encoding.UTF8))
                    setting = JsonSerializer.Deserialize<Setting>(await reader.ReadToEndAsync(), jsonOptions) ?? new();
            }
            catch (Exception ex)
            {
                Logger.Log(this, "Load failed");
                Logger.Log(this, ex.Message + Environment.NewLine);
            }
        }

        public async Task Handle(PluginInfo plugin, bool result, string message)
        {
            if (setting == null)
            {
                Logger.Log(this, "setting is empty.");
                return;
            }

            if (!result)
            {
                var content = $"{plugin.Name} had trouble trying to clock in, and here is the error message:\r\n\r\n" +
                              $"{plugin.Name} Info:\r\n" +
                              $"Author: {plugin.Author}\r\n" +
                              $"Description: {plugin.Description}\r\n" +
                              $"Version: {plugin.Version.ToString()}\r\n\r\n" +
                              message;

                string? errorMessage = null;
                await Task.Run(() =>
                {
                    try
                    {
                        MailSender.Send(setting.MailServer, setting.Key, setting.SendUser, setting.ReceiveUser, setting.Title, content, Array.Empty<string>(), false);
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.ToString();
                    }
                });

                Logger.Log(this, $"{(errorMessage == null ? "Success" : "Failed")} | Try to send email for the plugin \"{plugin.Name}\"." + Environment.NewLine + (errorMessage == null ? "" : errorMessage + Environment.NewLine));
            }
        }

    }
}