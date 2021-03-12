using System;
using System.Collections.Generic;
using System.Text;

namespace Griphon.Parser
{
    public class Tokenizer
    {
        private TokenIndex _index;
        private List<TexToken> _tokens = new List<TexToken>();

        public string Text { get; private set; }
        public bool Parsed { get; private set; } = true;

        public TokenIndex TokenIndex => new TokenIndex(_index);

        public bool HasMore => _index.Index < Text.Length;


        public char Current => Text[_index.Index];

        public List<TexToken> Tokens => new List<TexToken>(_tokens);

        public Tokenizer(string text)
        {
            Text = text;
        }


        public void Tokenize()
        {
            if(Parsed)
            {
                throw new InvalidOperationException("This tokenizer is already parsed.");
            }

            while(HasMore)
            {
                if(Current == TokenNames.BackSlash)
                {
                    _tokens.Add(ParseCommand());
                }

                if(Current == TokenNames.OpenBracket)
                {
                    _tokens.Add(ParseOpenBracket());

                    if(IsLetter())
                    {
                        _tokens.Add(ParseAttributeName());
                    }

                }


                if (Current == TokenNames.CloseBracket)
                {
                    _tokens.Add(ParseCloseBracket());
                }
            }

            if (Current == '=')
            {
                var token = ParseEqualToken();
            }
        }

        private TexToken ParseEqualToken()
        {
            if (Current != '=')
            {
                throw new InvalidOperationException();
            }

            return new TexToken("=", TokenType.Equal, _index);
        }

        private TexToken ParseCommand()
        {
            if (!HasMore || Current != '\\')
            {
                throw new InvalidOperationException();
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(Forward());

            if (IsLetter())
            {
                builder.Append(Forward());
            }
            else
            {
                throw new UnexpectedTokenException(this._index);
            }

            while (HasMore && IsIdentifierChar())
            {
                builder.Append(Forward());
            }

            return new TexToken(builder.ToString(), TokenType.Command, _index);
        }


        private TexToken ParseAttributeName()
        {
            if (!HasMore || !IsLetter())
            {
                throw new InvalidOperationException("Attribute name must start with a letter");
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(Forward());

            while (HasMore && IsIdentifierChar())
            {
                builder.Append(Forward());
            }

            return new TexToken(builder.ToString(), TokenType.AttributeName, _index);
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

            return new TexToken(builder.ToString(), TokenType.AttributeValue, _index);
        }

        public TexToken ParseOpenBracket()
        {
            if(!HasMore || Current != TokenNames.OpenBracket)
            {
                throw new InvalidOperationException($"Expected '{TokenNames.OpenBracket}' character.");
            }

            return new TexToken($"{TokenNames.OpenBracket}", TokenType.OpenBracket, _index);
        }


        public TexToken ParseCloseBracket()
        {
            if (!HasMore || Current != TokenNames.CloseBracket)
            {
                throw new InvalidOperationException($"Expected '{TokenNames.CloseBracket}' character.");
            }

            return new TexToken($"{TokenNames.CloseBracket}", TokenType.CloseBracket, _index);
        }



        public TexToken ParseOpenBrace()
        {
            if (!HasMore || Current != TokenNames.OpenBrace)
            {
                throw new InvalidOperationException($"Expected '{TokenNames.OpenBrace}' character.");
            }

            return new TexToken($"{TokenNames.OpenBrace}", TokenType.OpenBrace, _index);
        }


        public TexToken ParseCloseBrace()
        {
            if (!HasMore || Current != TokenNames.CloseBrace)
            {
                throw new InvalidOperationException($"Expected '{TokenNames.CloseBrace}' character.");
            }

            return new TexToken($"{TokenNames.CloseBrace}", TokenType.CloseBrace, _index);
        }


        /// <summary>
        /// Parse a html entity code like &#163, &#36; &pound;
        /// </summary>
        /// <returns></returns>
        public TexToken ParseHtmlCodeOrEntity()
        {
            if (!HasMore || Current != TokenNames.Ampersand)
            {
                throw new InvalidOperationException($"Expected '{TokenNames.Ampersand}' character at start of html entity or html code).");
            }
            
            if(HasMore && Current == TokenNames.Hash)
            {
                return ParseHtmlCode();
                
            }
            if (HasMore && IsLetter())
            {
                return ParseHtmlEntity();
            }
            throw new UnexpectedTokenException(_index);

        }

        public TexToken ParseHtmlCode()
        {
            if(!HasMore || Current != TokenNames.Hash)
            {
                throw new InvalidOperationException("The html entity must start with a &#");
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(TokenNames.Ampersand);
            builder.Append(TokenNames.Hash);

            if (!HasMore || !IsDigit())
            {
                throw new UnexpectedTokenException(this._index, "HTML_CODE_MALFORMED", "The html code must have only digit after the # character.");
            }

            builder.Append(Forward());

            while (HasMore && IsDigit())
            {
                builder.Append(Forward());
            }

            if (!HasMore)
            {
                throw new UnexpectedTokenException(this._index, "HTML_CODE_INCOMPLETED", "The html code is incompleted.");
            }

            if (Current != TokenNames.SemiColon)
            {
                throw new UnexpectedTokenException(this._index, "HTML_CODE_BAD_END", "The html code must end with the semicolon(;) character.");
            }

            builder.Append(Forward());
            return new TexToken(builder.ToString(), TokenType.HtmlCode, _index);
        }


        public TexToken ParseHtmlEntity()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(TokenNames.Ampersand);

            if (!HasMore || !IsLetter())
            {
                throw new UnexpectedTokenException(this._index, "HTML_ENTITY_MALFORMED", "The html entity must have only letter after the & character.");
            }

            builder.Append(Forward());

            while (HasMore && IsLetter())
            {
                builder.Append(Forward());
            }

            if (!HasMore)
            {
                throw new UnexpectedTokenException(this._index, "HTML_ENTITY_INCOMPLETED", "The html entity is incompleted.");
            }

            if (Current != TokenNames.SemiColon)
            {
                throw new UnexpectedTokenException(this._index, "HTML_ENTITY_BAD_END", "The html enitty must end with the semicolon(;) character.");
            }

            builder.Append(Forward());
            return new TexToken(builder.ToString(), TokenType.HtmlEntity, _index);
        }

        /// <summary>
        /// Parse the comment.
        /// The comment start with %% and end with %%;
        /// </summary>
        /// <returns></returns>
        public TexToken ParseComment()
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
            return new TexToken(builder.ToString(), TokenType.Comment, _index);
        }


        public TexToken ParseMathExpression()
        {
            if (!HasMore || Current != TokenNames.Dollar)
            {
                throw new InvalidOperationException();
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(Forward());

            while(HasMore && Current != TokenNames.Dollar)
            {
                builder.Append(Forward());
            }

            if (!HasMore || Current != TokenNames.Dollar)
            {
                throw new ExceptedTokenException("MATH_END_ERROR", "The math expression must end with a '$' character");
            }
            builder.Append(Forward());

            return new TexToken(builder.ToString(), TokenType.MathExpression, _index);
        }

        private char Forward()
        {
            char value;

            if (HasMore)
            {
                value = Current;
                _index.LineIndex += 1;
                _index.Index += 1;
                return value;
            }

            throw new IndexOutOfRangeException($"There are no token at line ({_index.Line}, {_index.LineIndex}).");
        }


        /// <summary>
        /// Move cursor to the next char.
        /// </summary>
        /// <returns>
        ///   <code>true</code> if the cursor has been moved.
        ///   <code>false</code> otherwise.
        /// </returns>
        private bool Next()
        {
            if (HasMore)
            {
                _index.LineIndex += 1;
                _index.Index += 1;
                return true;
            }
            return false;
        }


        bool IsDigit()
        {
            return Current >= '0' && Current <= '9';
        }

        bool IsLetter()
        {
            return (Current >= 'a' && Current <= 'z' || (Current >= 'A' && Current <= 'Z'));
        }

        bool IsIdentifierChar()
        {
            return IsLetter() || IsDigit() || Current == '_' || Current == '-';
        }


        bool IsCurrent(char character)
        {
            return HasMore && Current == character;
        }

        private void ThrowUnExpectedToken(char? character)
        {

        }
    }
}
