using System.Text;
using Griphon.Parser.Tokens;

namespace Griphon.Parser
{
    public class TextTokenStream
    {
        private ParseStream stream;

        private StringBuilder builder;

        public TextTokenStream(ParseStream stream)
        {
            this.stream = stream;
            builder = new StringBuilder();
        }

        public void Take()
        {
            builder.Append(stream.Current);
        }
        
        public void Take(char character)
        {
            builder.Append(character);
        }

        public TextToken Stop()
        {
            if (builder.Length == 0)
            {
                return null;
            }
            TextToken token = new TextToken
            {
                Index = stream.Index,
                Value = builder.ToString()
            };

            builder.Clear();

            return token;
        }
    }
}