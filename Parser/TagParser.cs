using System;
using System.Text;

namespace Griphon.Parser
{
    public class TagParser
    {
        public TexToken Parse(ParseStream stream)
        {
            if (!stream.HasMore || stream.Current != '\\')
            {
                throw new InvalidOperationException();
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(stream.Forward());

            if (stream.IsLetter())
            {
                builder.Append(stream.Forward());
            }
            else
            {
                throw new UnexpectedTokenException(stream.Index);
            }

            while (stream.HasMore && stream.IsIdentifierChar())
            {
                builder.Append(stream.Forward());
            }

            return new TexToken(builder.ToString(), TokenType.Command, stream.Index);
        }
    }
}