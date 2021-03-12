using System;
using System.Collections.Generic;
using System.Text;

namespace Griphon.Parser
{
    public class UnexpectedTokenException: GriphonException
    {
        public char? Character { get; set; }

        public string Value { get; set; }

        public TokenIndex TokenIndex { get; set; }


        public UnexpectedTokenException(TokenIndex index, char? character)
            : base("UNEXPECTED_CHARACTER_ERROR", $"Unexpected token \"{character}\" at line ({index.Line}, {index.LineIndex}).")
        {
            Character = character;
            TokenIndex = index;
        }


        public UnexpectedTokenException(TokenIndex index, string value)
            : base("UNEXPECTED_TOKEN_ERROR", $"Unexpected token \"{value}\" at line ({index.Line}, {index.LineIndex}).")
        {
            Value = value;
            TokenIndex = index;
        }

        public UnexpectedTokenException(TokenIndex index)
            : base("UNEXPECTED_TOKEN_ERROR", $"Unexpected token at line ({index.Line}, {index.LineIndex}).")
        {
            TokenIndex = index;
        }

        public UnexpectedTokenException(TokenIndex index, string code, string message): base(code, message)
        {
           TokenIndex = index;
        }
    }
}
