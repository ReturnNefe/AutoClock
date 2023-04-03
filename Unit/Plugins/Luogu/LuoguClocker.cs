using System.Text.Json;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Text.Json.Nodes;
using Flurl.Http;
using AutoClock.Interface;

namespace Luogu
{
    public class LuoguClocker : IClocker
    {
        // Private Properties
        private PluginInfo info = new("Luogu", "ReturnNefe", "A clocker for Luogu.", new("0.1.0"));
        private DateTime final = DateTime.Now;
        private Setting? setting;

        // Private Method
        private async Task<string> GetSingleTagValueByAttr(string htmlText, string tagName, string attrName, string key)
        {
            return await Task.Run(() =>
            {
                var regText = $"(?<={tagName} {attrName}=\"{key}\" content=\").*?(?=\")";
                var reg = new Regex(regText, RegexOptions.IgnoreCase);
                var matchs = reg.Matches(htmlText);

                foreach (Match match in matchs)
                {
                    var matchValue = match.Value;
                    if (!string.IsNullOrEmpty(matchValue))
                        return matchValue;
                }

                return "";
            });
        }

        private async Task<string> GetLuoguCsrfToken(string cookie)
        {
            if (setting == null)
            {
                Logger.Log(this, "setting is empty.");
                return "";
            }
            
            return await GetSingleTagValueByAttr(await setting.GetTokenUrl
                                                                  .WithHeader("cookie", cookie)
                                                                  .WithHeader("referer", setting.PostReferer)
                                                                  .WithHeader("user-agent", setting.PostUserAgent)
                                                                  .GetStringAsync(),
                                                 "meta", "name", "csrf-token");
        }

        // Public Properties
        public PluginInfo Info => info;

        // Public Method
        public LuoguClocker() { }

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

        public async Task<(bool, string)> Clock()
        {
            if (setting == null)
            {
                Logger.Log(this, "setting is empty.");
                return (true, "setting error");
            }
            
            var index = 0;
            var success = true;
            var error = new StringBuilder();

            foreach (var user in setting.Users)
            {
                try
                {
                    var result = await (await setting.PunchUrl
                                                      .WithHeader("cookie", user.Cookie)
                                                      .WithHeader("x-csrf-token", await GetLuoguCsrfToken(user.Cookie))
                                                      .WithHeader("referer", setting.PostReferer)
                                                      .WithHeader("user-agent", setting.PostUserAgent)
                                                      .PostAsync())
                                              .GetStringAsync();

                    var resultJson = JsonSerializer.Deserialize<JsonObject>(result);
                    if (resultJson == null)
                        throw new Exception("result is null");

                    if ((int)resultJson["code"]! == 200)
                    {
                        Logger.Log(this, $"User-{index} 打卡成功{Environment.NewLine}");
                    }
                    else
                    {
                        success = false;
                        throw new Exception($"User-{index} 打卡失败{Environment.NewLine}");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(this, ex.ToString() + Environment.NewLine);
                    error.AppendLine($"[User-{index}] " + ex.ToString() + Environment.NewLine);
                }
                
                ++index;
            }

            return (success, error.ToString());
        }

        public async Task Wait()
        {
            if (DateTime.Now > final)
            {
                final += TimeSpan.FromDays(1);
                return;
            }
            else
                await Task.Delay(final - DateTime.Now);
        }
    }
}