namespace AutoClock.Interface
{
    public interface IMonitor : IPlugin
    {
        public Task Handle(PluginInfo plugin, bool result, string message);
    }
}