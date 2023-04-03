namespace AutoClock.Interface
{
    public interface Plugin
    {
        public PluginInfo Info { get; }
        
        public Task Loading(string baseDirectory);
    }
}