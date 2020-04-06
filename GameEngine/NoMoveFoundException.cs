using System;
using System.Runtime.Serialization;

namespace GameEngine
{
    public class NoMoveFoundException : Exception
    {
        public NoMoveFoundException() : base() { }
        public NoMoveFoundException(string message) : base(message) { }
        public NoMoveFoundException(string message, Exception inner) : base(message, inner) { }
        protected NoMoveFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
