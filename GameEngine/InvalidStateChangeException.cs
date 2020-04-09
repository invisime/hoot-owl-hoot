using System;
using System.Runtime.Serialization;

namespace GameEngine
{
    public class InvalidStateChangeException : Exception
    {
        public InvalidStateChangeException() : base() { }
        public InvalidStateChangeException(string message) : base(message) { }
        public InvalidStateChangeException(string message, Exception inner) : base(message, inner) { }
        protected InvalidStateChangeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
