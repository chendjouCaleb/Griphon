using System;
using System.Text;
using Griphon.Parser.Tokens;

namespace Griphon.Parser.Parsers
{
    public class TagParser: Parser<TagToken>
    {
        public TagParser(ParseStream stream) : base(stream)
        {
        }

        public override TagToken Parse()
        {
            if (!stream.HasMore || stream.Current != '/')
            {
                throw new InvalidOperationException();
            }

            TagToken token = new TagToken { Index = stream.Index };

            StringBuilder builder = new StringBuilder();
            builder.Append(stream.Current);
            stream.Next();

            if (stream.IsLetter())
            {
                builder.Append(stream.Current);
            }
            else
            {
                throw new UnexpectedTokenException(stream.Index, Current);
            }

            while (stream.Next() && stream.IsIdentifierChar())
            {
                builder.Append(stream.Current);
            }

            token.Value = builder.ToString();
            token.TagName = token.Value.Substring(1);

            return token;
        }
    }
}