using System;
using System.Runtime.Serialization;

namespace GameEngine
{
    public class StateMismatchException : Exception
    {
        public StateMismatchException() : base() { }
        public StateMismatchException(string message) : base(message) { }
        public StateMismatchException(string message, System.Exception inner) : base(message, inner) { }
        protected StateMismatchException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
