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

        public Task<(bool, string)> Clock()
        {
            throw new NotImplementedException();
        }

        public Task Loading(string baseDirectory)
        {
            throw new NotImplementedException();
        }

        public Task Wait()
        {
            throw new NotImplementedException();
        }
    }
}