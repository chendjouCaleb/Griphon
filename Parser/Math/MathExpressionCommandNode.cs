using System.Collections.Generic;

namespace Griphon.Parser.Math
{
    public class MathExpressionCommandNode
    {
        public string Name { get; set; }
        

        public List<MathExpressionNode> Children { get; set; }
    }
}