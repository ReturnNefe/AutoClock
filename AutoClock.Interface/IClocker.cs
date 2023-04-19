namespace AutoClock.Interface
{
    public interface IClocker : IPlugin
    {
        /// <summary>
        /// Occur when Wait() return.
        /// </summary>
        /// <returns>
        /// bool: True if all is done, Otherwise, False.
        /// string: Detail message. 
        /// </returns>
        public Task<(bool, string)> Clock();
        
        public Task Wait();
    }
}