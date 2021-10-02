using System;
using System.Text;
using Griphon.Parser.Tokens;

namespace Griphon.Parser.Parsers
{
    public class AttributeListParser: Parser<AttributeListToken>
    {
        public AttributeListParser(ParseStream stream) : base(stream)
        {
        }
        
        public override AttributeListToken Parse()
        {
            throw new System.NotImplementedException();
        }
        
        
        private TexToken ParseAttributeName()
        {
            if (!HasMore || !IsLetter)
            {
                throw new InvalidOperationException("Attribute name must start with a letter");
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(Forward());

            while (HasMore && IsIdentifierChar)
            {
                builder.Append(Forward());
            }

            return new TexToken(builder.ToString(), TokenType.AttributeName, stream.Index);
        }

        private TexToken ParseAttributeValue()
        {
            if (!HasMore || IsCurrent(TokenNames.QuoteMarks))
            {
                throw new InvalidOperationException($"Attribute value must start with a quoteMarks(\")");
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(Forward());

            while(HasMore && Current != TokenNames.QuoteMarks)
            {
                builder.Append(Forward());
            }

            if (!HasMore || Current != TokenNames.QuoteMarks)
            {
                throw new ExceptedTokenException("ATTRIBUTE_VALUE_END_ERROR", $"Attribute value must end with a quoteMarks(\")");
            }
            builder.Append(Forward());

            return new TexToken(builder.ToString(), TokenType.AttributeValue, stream.Index);
        }

        
        
    }
}