namespace ClawLibrary.Core.Services
{
    /// <summary>
    /// Defines an object that acts as a context for the currently logged in user.
    /// </summary>
    public interface ISessionContext
    {
        /// <summary>
        /// The ID of the currently logged in user. When the user is unauthenticated this property will return 0.
        /// </summary>
        string UserId { get; }
        /// <summary>
        /// Gets the user email.
        /// </summary>
        string UserEmail { get; }
        /// <summary>
        /// Language.
        /// </summary>
        string Language { get; }
    }
}