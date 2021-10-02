using System;
using System.Text;
using Griphon.Parser.Tokens;

namespace Griphon.Parser.Parsers
{
    public class CommentParser: Parser<CommentToken>
    {
        public CommentParser(ParseStream stream) : base(stream)
        {
        }

        public override CommentToken Parse()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(TokenNames.Percent);

            if(!HasMore || Current != TokenNames.Percent)
            {
                throw new InvalidOperationException($"Comment must starts with '%%'.");
            }
            builder.Append(Forward());

            while(HasMore && Current != TokenNames.Percent)
            {
                builder.Append(Forward());

            }

            while(HasMore && Current == TokenNames.Percent)
            {
                builder.Append(Forward());
                if (!HasMore)
                {
                    throw new ExceptedTokenException("COMMENT_BAD_END", "Comment must ends with %%");
                }
                //Eat the second %
                if (HasMore && Current == TokenNames.Percent)
                {
                    builder.Append(Forward());
                    break;
                }
                while (HasMore && Current != TokenNames.Percent)
                {
                    builder.Append(Forward());
                }
            }

            CommentToken token = new CommentToken {Value = builder.ToString(), Index = stream.Index};
            token.Comment = token.Value.Substring(1, token.Value.Length - 4);
            return token;
        }
    }
}