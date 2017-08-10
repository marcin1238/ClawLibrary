using System;

namespace ClawLibrary.Core.Exceptions
{
    [Serializable]
    public class BusinessExceptionPayload
    {
        public object Data { get; set; }
    }
}