using Griphon.Parser.Tokens;

namespace Griphon.Parser.Parsers
{
    public abstract class Parser<T> where T:Token
    {
        protected ParseStream stream;
        public Parser(ParseStream stream)
        {
            this.stream = stream;
        }
        public abstract T Parse();


        public bool HasMore => stream.HasMore;

        public TokenIndex Index => stream.Index;
        public char Current => stream.Current;
        public bool IsLetter => stream.IsLetter();
        public bool IsDigit() => stream.IsLetter();

        public bool IsCurrent(char value)
        {
            return stream.IsCurrent(value);
        }

        public bool IsIdentifierChar => stream.IsIdentifierChar();

        public char Forward()
        {
            return stream.Forward();
        }
        
        
    }
}