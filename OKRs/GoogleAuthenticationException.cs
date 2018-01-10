using System;
using System.Runtime.Serialization;

namespace OKRs
{
    [Serializable]
    internal class GoogleAuthenticationException : Exception
    {
        public GoogleAuthenticationException()
        {
        }

        public GoogleAuthenticationException(string message) : base(message)
        {
        }

        public GoogleAuthenticationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GoogleAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}