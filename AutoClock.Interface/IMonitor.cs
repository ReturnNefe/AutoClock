namespace AutoClock.Interface
{
    public interface IMonitor : Plugin
    {
        public Task Handle(PluginInfo plugin, bool result, string message);
    }
}