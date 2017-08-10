using System;

namespace ClawLibrary.Core.Exceptions
{
    /// <summary>
    /// Exception to be thrown when a business rule is violated. This exception is intended to bubble up to the client.
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        public BusinessException()
        {

        }

        public BusinessException(string message, BusinessExceptionPayload details = null) : base(message)
        {
            Details = details;
        }

        public BusinessException(string message, Exception inner, BusinessExceptionPayload details = null)
            : base(message, inner)
        {
            Details = details;
        }

        public BusinessException(Enum errorCode, BusinessExceptionPayload details = null)
        {
            ErrorCode = errorCode;
            Details = details;
        }

        public BusinessException(Enum errorCode, string message, BusinessExceptionPayload details = null)
            : base(message)
        {
            ErrorCode = errorCode;
            Details = details;
        }

        public Enum ErrorCode { get; protected set; }
        public BusinessExceptionPayload Details { get; protected set; }
    }
}
