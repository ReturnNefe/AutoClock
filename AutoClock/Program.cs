using System;
using System.Linq;
using Nefe.PluginCore;
using AutoClock.Global;
using AutoClock.Interface;

namespace AutoClock
{
    internal class Program
    {
        static async Task LoadPlugin(string directory)
        {
            foreach (var dirPath in Directory.GetDirectories(directory))
            {
                var pluginName = Path.GetFileName(dirPath);
                var pluginFile = Path.Combine(dirPath, $"{pluginName}.dll");

                if (File.Exists(pluginFile))
                {
                    var plugin = new Nefe.PluginCore.Plugin(pluginFile, isCollectible: true);
                    plugin.LoadFromFile();
                    AppInfo.Plugins.Add(plugin);

                    var clockers = plugin.CreateInstances<IClocker>();
                    var monitors = plugin.CreateInstances<IMonitor>();
                    foreach (var iter in clockers)
                    {
                        await iter.OnLoading(dirPath);
                        plugin.Unloading += async (_) => await iter.OnUnloading();
                        AppInfo.Clockers.Add(iter);
                    }
                    foreach (var iter in monitors)
                    {
                        await iter.OnLoading(dirPath);
                        plugin.Unloading += async (_) => await iter.OnUnloading();
                        AppInfo.Monitors.Add(iter);
                    }

                    var logMessage = string.Empty;
                    if (clockers.Count() > 0)
                        logMessage += $"{clockers.Count()} Clocker(s) ";
                    if (monitors.Count() > 0)
                        logMessage += $"{monitors.Count()} Monitor(s) ";

                    Logger.Log("AutoClock", $"{pluginName} had been loaded. ({(string.IsNullOrEmpty(logMessage) ? "No types" : logMessage[..^1])})");
                }
            }

            Logger.NextLine();
        }

        static async Task Main(string[] args)
        {
            // Unload the loaded plugins when Console exit
            Console.CancelKeyPress += (sender, e) =>
            {
                foreach (var iter in AppInfo.Plugins)
                    iter.Unload();
            };

            await LoadPlugin(Path.Combine(AppInfo.Path, "plugins"));

            foreach (var clocker in AppInfo.Clockers)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        while (true)
                        {
                            await clocker.Wait();

                            try
                            {
                                var result = await clocker.Clock();

                                foreach (var monitor in AppInfo.Monitors)
                                {
                                    try
                                    {
                                        _ = monitor.Handle(clocker.Info, result.Item1, result.Item2);
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.Log("Console.Monitor", ex.ToString());
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.Log("Console.Clock", ex.ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Console", ex.ToString());
                    }
                });
            }

            new ManualResetEvent(false).WaitOne();
        }
    }
}