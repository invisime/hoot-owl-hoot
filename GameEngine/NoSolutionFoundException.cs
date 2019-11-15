using System;
using System.Runtime.Serialization;

namespace GameEngine
{
    public class NoSolutionFoundException : Exception
    {
        public NoSolutionFoundException() : base() { }
        public NoSolutionFoundException(string message) : base(message) { }
        public NoSolutionFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected NoSolutionFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
