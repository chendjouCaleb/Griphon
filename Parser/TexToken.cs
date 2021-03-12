using System;
using System.Collections.Generic;
using System.Text;

namespace Griphon.Parser
{
    public struct TexToken
    {
        public TexToken(string text, TokenType type, TokenIndex index)
        {
            Text = text;
            Type = type;
            Index = index.Index;
            Line = index.Line;
            LineIndex = index.LineIndex;

        }

        public readonly string Text { get; }
        public readonly TokenType Type { get; }
        public readonly int Index { get; }
        public readonly int Line { get; }
        public readonly int LineIndex { get; }
    }
}
