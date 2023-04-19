namespace AutoClock.Interface
{
    public interface IPlugin : IEventPlugin
    {
        public PluginInfo Info { get; }
    }
}