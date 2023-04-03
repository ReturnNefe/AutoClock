using System;
using Nefe.PluginCore;
using AutoClock.Global;
using AutoClock.Interface;
using System.Linq;

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
                    var plugin = new Nefe.PluginCore.Plugin();
                    plugin.LoadFromFile(pluginFile);
                    AppInfo.Plugins.Add(plugin);

                    var clockers = plugin.CreateInstances<IClocker>();
                    var monitors = plugin.CreateInstances<IMonitor>();
                    foreach (var iter in clockers)
                    {
                        await iter.Loading(dirPath);
                        AppInfo.Clockers.Add(iter);
                    }
                    foreach (var iter in monitors)
                    {
                        await iter.Loading(dirPath);
                        AppInfo.Monitors.Add(iter);
                    }
                    
                    Logger.Log("AutoClock", $"{pluginName} had been loaded. Created {clockers.Count()} Clocker(s), {monitors.Count()} Monitor(s).");
                }
            }
            
            Logger.NextLine();
        }

        static async Task Main(string[] args)
        {
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