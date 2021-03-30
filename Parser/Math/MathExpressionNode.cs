using System.Collections.Generic;

namespace Griphon.Parser.Math
{
    public abstract class MathExpressionNode
    {
        public MathExpressionNode Parent { get; set; }

        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

        public int Index { get; set; }

        public abstract string NodeType { get; }
        
        
    }
}