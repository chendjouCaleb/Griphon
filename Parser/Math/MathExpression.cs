using System.Collections.Generic;

namespace Griphon.Parser.Math
{
    public class MathExpression
    {
        public string ExpressionString { get; set; }

        public int Depth { get; set; }

        public List<MathExpressionNode> Nodes { get; set; }
    }
}