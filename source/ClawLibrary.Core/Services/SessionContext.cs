namespace ClawLibrary.Core.Services
{
    /// <summary>
    /// A context object for the current session that contains useful system wide information like currently logged in UserId etc.  This object is intended to 
    /// exist for the lifetime of a Http Request.
    /// </summary>
    public class SessionContext : ISessionContext
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string Language { get; set; }
    }
}