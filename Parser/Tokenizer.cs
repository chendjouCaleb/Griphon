using System;
using System.Collections.Generic;
using System.Text;
using Griphon.Parser.Parsers;
using Griphon.Parser.Tokens;

namespace Griphon.Parser
{
    public class Tokenizer
    {
        private List<Token> _tokens = new List<Token>();
        private ParseStream Stream;

        public string Text { get; }
        public bool Parsed { get; private set; } = true;

        public int BraceDepth { get; private set; }
        
        public List<Token> Tokens => new List<Token>(_tokens);

        private TextTokenStream TextTokenStream;

        public Tokenizer(string text)
        {
            Text = text;
            Stream = new ParseStream(text);
            TextTokenStream = new TextTokenStream(Stream);
        }


        public void Tokenize()
        {
            if(Parsed)
            {
                throw new InvalidOperationException("This tokenizer is already parsed.");
            }

            while(Stream.HasMore)
            {
                if(Stream.Current == TokenNames.BackSlash)
                {
                    AddToken(TextTokenStream.Stop());
                    _tokens.Add(ParseTag());
                }

                else if(Stream.Current == TokenNames.OpenBracket)
                {
                    _tokens.Add(ParseAttributeList());
                }
                
                else if (Stream.Current == TokenNames.CloseBrace)
                {
                    _tokens.Add(ParseOpenBrace());
                }

                else if (Stream.Current == TokenNames.CloseBrace)
                {
                    _tokens.Add(ParseCloseBrace());
                }

                else if (Stream.Current == TokenNames.Percent)
                {
                    if (Stream.HasMore && Stream.Forward() == TokenNames.Percent)
                    {
                        _tokens.Add(ParseComment());
                    }
                    else
                    {
                        TextTokenStream.Take(TokenNames.Percent);
                    }
                }

                else if (Stream.Current == TokenNames.Ampersand)
                {
                    AddToken(TextTokenStream.Stop());
                    AddToken(ParseHtmlCodeOrEntity());
                }
                else
                {
                    TextTokenStream.Take();
                }
            }
            AddToken(TextTokenStream.Stop());

            Parsed = true;
        }

        private void AddToken(Token token)
        {
            if (token != null)
            {
                _tokens.Add(token);
            }
        }

        private TagToken ParseTag()
        {
            return new TagParser(Stream).Parse();
        }

        private AttributeListToken ParseAttributeList()
        {
            return new AttributeListParser(Stream).Parse();
        }

        private CharToken ParseOpenBrace()
        {
            CharToken token = new CharToken(Stream.Current, Stream.Index);
            BraceDepth++;

            if (!Stream.HasMore)
            {
                throw new ExceptedTokenException("UNCLOSED_BRACE", "The brace must be closed");
            }

            return token;
        }

        private CharToken ParseCloseBrace()
        {
            if (BraceDepth <= 0)
            {
                throw new ExceptedTokenException("OPEN_BRACE_NOT_FOUND", "Cannot close unopened brace");
            }
            CharToken token = new CharToken(Stream.Current, Stream.Index);
            BraceDepth--;

            return token;
        }

        private CommentToken ParseComment()
        {
            return new CommentParser(Stream).Parse();
        }


        /// <summary>
        /// Parse a html entity code like &#163, &#36; &pound;
        /// </summary>
        /// <returns></returns>
        public Token ParseHtmlCodeOrEntity()
        {
            if (!Stream.HasMore || Stream.Current != TokenNames.Ampersand)
            {
                throw new InvalidOperationException($"Expected '{TokenNames.Ampersand}' character at start of html entity or html code).");
            }
            
            if(Stream.HasMore && Stream.Forward() == TokenNames.Hash)
            {
                return ParseHtmlCode();
            }
            if (Stream.HasMore && Stream.IsLetter())
            {
                return ParseHtmlEntity();
            }
            throw new UnexpectedTokenException(Stream.Index);

        }

        public HtmlCodeToken ParseHtmlCode()
        {
            return new HtmlCodeParser(Stream).Parse();
        }


        public HtmlEntityToken ParseHtmlEntity()
        {
            return new HtmlEntityParser(Stream).Parse();
        }
    }
}
