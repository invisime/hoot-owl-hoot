using System;
using System.Runtime.Serialization;

namespace GameEngine
{
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException() : base() { }
        public InvalidMoveException(string message) : base(message) { }
        public InvalidMoveException(string message, System.Exception inner) : base(message, inner) { }
        protected InvalidMoveException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
