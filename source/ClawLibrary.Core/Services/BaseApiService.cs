namespace ClawLibrary.Core.Services
{
    /// <summary>
    /// Defines a base class for all application services in the system
    /// </summary>
    public abstract class BaseApiService
    {
        private ISessionContextProvider _provider;

        protected BaseApiService(ISessionContextProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Provides a context for the current user session.  This provides an easy way to access the current user id, relationship id, etc.
        /// </summary>
        protected ISessionContext Session => _provider.GetContext();
    }
}