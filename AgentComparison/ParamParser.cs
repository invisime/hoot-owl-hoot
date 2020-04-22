using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AgentComparison
{
    public class ParserException : Exception
    {
        public ParserException() : base() { }
        public ParserException(string message) : base(message) { }
        public ParserException(string message, Exception inner) : base(message, inner) { }
        protected ParserException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class ParamParser
    {
        private string _input;
        private int _index;

        public IReadOnlyDictionary<string, string> Parse(string input)
        {
            _index = 0;
            _input = input;

            var ret = new Dictionary<string, string>();

            while(_index < _input.Length)
            {
                Expect("--");
                var paramName = Symbol();
                Expect("=");
                var param = Symbol();
                ret.Add(paramName, param);
            }

            return ret;
        }

        private string Symbol()
        {
            bool IsSymbolChar(char c) => char.IsLetterOrDigit(c) || c == '_' || c == '-';

            ClearWhitespace();

            var buffer = new System.Text.StringBuilder();
            while(_index < _input.Length && IsSymbolChar(_input[_index]))
            {
                buffer.Append(_input[_index]);
                _index++;
            }
            return buffer.ToString();
        }

        private void Expect(string str)
        {
            ClearWhitespace();

            var i = 0;
            while(_index < _input.Length && i < str.Length)
            {
                if(_input[_index] != str[i])
                {
                    throw new ParserException($"Expected {str[i]} in {str} but found {_input[_index]}");
                }
                _index++;
                i++;
            }
        }

        private void ClearWhitespace()
        {
            while(char.IsWhiteSpace(_input[_index]))
            {
                _index++;
            }
        }
    }
}
