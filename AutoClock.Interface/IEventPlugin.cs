namespace AutoClock.Interface
{
    public interface IEventPlugin
    {
        public Task OnLoading(string baseDirectory);
        public Task OnUnloading();
    }
}