namespace ClawLibrary.Core.Services
{
    /// <summary>
    /// Defines a provider that gets the current <see cref="ISessionContext" />
    /// </summary>
    public interface ISessionContextProvider
    {
        /// <summary>
        /// Gets the current session context
        /// </summary>
        /// <returns></returns>
        ISessionContext GetContext();
    }
}