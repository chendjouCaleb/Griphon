using System.Text;
using Griphon.Parser.Tokens;

namespace Griphon.Parser.Parsers
{
    public class HtmlEntityParser:Parser<HtmlEntityToken>
    {
        public HtmlEntityParser(ParseStream stream) : base(stream)
        {
        }
        public override HtmlEntityToken Parse()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(TokenNames.Ampersand);

            if (!HasMore || !IsLetter)
            {
                throw new UnexpectedTokenException(Index, "HTML_ENTITY_MALFORMED", "The html entity must have only letter after the & character.");
            }

            builder.Append(Forward());

            while (HasMore && IsLetter)
            {
                builder.Append(Forward());
            }

            if (!HasMore)
            {
                throw new UnexpectedTokenException(this.Index, "HTML_ENTITY_INCOMPLETED", "The html entity is incompleted.");
            }

            if (Current != TokenNames.SemiColon)
            {
                throw new UnexpectedTokenException(Index, "HTML_ENTITY_BAD_END", "The html enitty must end with the semicolon(;) character.");
            }

            builder.Append(Forward());

            HtmlEntityToken token = new HtmlEntityToken {Index = Index, Value = builder.ToString()};
            token.Entity = token.Value.Substring(1);
            return token;
        }

        
    }
}