using Flurl.Http;
using AutoClock.Interface;

namespace Bilibili
{
    public class BilibiliClocker : IClocker
    {
        // Private Properties
        private PluginInfo info = new("Bilibili", "ReturnNefe", "A clocker for Bilibili.", new("0.1.0"));
        private DateTime final = DateTime.Now;
        
        // Public Properties
        public PluginInfo Info => info;

        public Task OnLoading(string baseDirectory) => Task.CompletedTask;

        public Task OnUnloading() => Task.CompletedTask;

        public Task<(bool, string)> Clock()
        {
            return Task.FromResult((true, ""));
        }

        public Task Wait()
        {
            return Task.CompletedTask;
        }
    }
}