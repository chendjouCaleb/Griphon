namespace Griphon.Parser.Math
{
    public class MathExpressionTextNode:MathExpressionNode
    {
        public override string NodeType => "text";

        public string Content { get; set; }
    }
}