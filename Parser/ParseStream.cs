namespace Griphon.Parser
{
    public class ParseStream
    {
        private TokenIndex _index = new TokenIndex();
        public bool Launched { get; private set; }
        public string Text { get; }
        public TokenIndex Index => new TokenIndex(_index);

        public bool HasMore => _index.Index < Text.Length - 1;
        public bool IsLast => Text.Length == 0 || _index.Index == Text.Length - 1;
        
        public char Current => Text[_index.Index];


        public ParseStream(string text)
        {
            Text = text;
        }

        public bool IsCurrent(char character)
        {
            return Current == character;
        }
        public bool IsLetter()
        {
            return Current >= 'a' && Current <= 'z' || (Current >= 'A' && Current <= 'Z');
        }
        
        public bool IsIdentifierChar()
        {
            return IsLetter() || IsDigit() || Current == '_' || Current == '-';
        }
        
        public bool IsDigit()
        {
            return Current >= '0' && Current <= '9';
        }
        
        /// <summary>
        /// Move cursor to the next char.
        /// </summary>
        /// <returns>
        ///   <code>true</code> if the cursor has been moved.
        ///   <code>false</code> otherwise.
        /// </returns>
        public bool Next()
        {
            if (HasMore)
            {
                _index.LineIndex += 1;
                _index.Index += 1;
                return true;
            }
            return false;
        }
        
        public char Forward()
        {
            if (HasMore)
            {
                Next();
                return Current;
            }
            return '\0';

            //throw new IndexOutOfRangeException($"There are no token at line ({_index.Line}, {_index.LineIndex}).");
        }
    }
}