using System;
using System.Text;
using Griphon.Parser.Tokens;

namespace Griphon.Parser.Parsers
{
    public class HtmlCodeParser:Parser<HtmlCodeToken>
    {
        public override HtmlCodeToken Parse()
        {
            if(!HasMore || Current != TokenNames.Hash)
            {
                throw new InvalidOperationException("The html entity must start with a &#");
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(TokenNames.Ampersand);
            builder.Append(TokenNames.Hash);

            if (!HasMore || !stream.IsDigit())
            {
                throw new UnexpectedTokenException(Index, "HTML_CODE_MALFORMED", "The html code must have only digit after the # character.");
            }

            builder.Append(Forward());

            while (HasMore && IsDigit())
            {
                builder.Append(Forward());
            }

            if (!HasMore)
            {
                throw new UnexpectedTokenException(Index, "HTML_CODE_INCOMPLETED", "The html code is incompleted.");
            }

            if (Current != TokenNames.SemiColon)
            {
                throw new UnexpectedTokenException(Index, "HTML_CODE_BAD_END", "The html code must end with the semicolon(;) character.");
            }

            builder.Append(Forward());

            HtmlCodeToken token = new HtmlCodeToken {Index = stream.Index, Value = builder.ToString()};
            token.Code = token.Value.Substring(1);
            return token;
        }

        public HtmlCodeParser(ParseStream stream) : base(stream) { }
    }
}