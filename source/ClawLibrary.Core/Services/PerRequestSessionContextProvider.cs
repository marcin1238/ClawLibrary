using System;
using System.Web;

namespace ClawLibrary.Core.Services
{
    /// <summary>
    /// A provider that stores and retrieves the current <see cref="ISessionContext" /> from the ASP.Net Per-Request cache.
    /// </summary>
    public class PerRequestSessionContextProvider : ISessionContextProvider
    {
        private const string ContextKey = "PerRequestSessionContextProvider";

        /// <summary>
        /// A per thread SessionContext cache to be used when HttpContext.Current is not available (i.e. during integration testing).
        /// </summary>
        [ThreadStatic]
        private static ISessionContext _perThreadSessionContext;

        /// <summary>
        /// Sets the context for the current Http Request.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void SetContext(ISessionContext context)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items[ContextKey] = context;
            else
                _perThreadSessionContext = context;
        }

        /// <summary>
        /// Gets the current session context
        /// </summary>
        /// <returns></returns>
        public ISessionContext GetContext()
        {
            if (HttpContext.Current != null)
                return (HttpContext.Current.Items[ContextKey] as ISessionContext) ?? null;

            return _perThreadSessionContext ?? null;
        }

        private ISessionContext GetDefault()
        {
            // Return the default configuration of the SessionContext
            return new SessionContext();
        }
    }
}
