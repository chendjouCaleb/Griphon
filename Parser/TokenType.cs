using System;
using System.Collections.Generic;
using System.Text;

namespace Griphon.Parser
{
    public enum TokenType
    {
        Identifier,
        Text,
        Comma,
        Dot,
        Percent,
        Caret,
        Underscore,
        QuoteMarks,
        Apostrophe,

        LineEnd,
        Tab,
        Equal,

        Command,
        AttributeName,
        AttributeValue,

        OpenBracket,
        CloseBracket,

        OpenBrace,
        CloseBrace,

        HtmlEntity,
        HtmlCode,

        Comment,

        MathExpression


    }
}
