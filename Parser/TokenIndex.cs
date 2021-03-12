

namespace Griphon.Parser
{
    public class TokenIndex
    {

        public TokenIndex()
        {

        }

        public TokenIndex(TokenIndex index)
        {
            Line = index.Line;
            LineIndex = index.LineIndex;
            Index = index.Index;
        }

        public int Line { get; set; }
        public int LineIndex { get; set; }

        public int Index { get; set; }
    }
}
