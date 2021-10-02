using System.Collections.Generic;

namespace Griphon.Parser.Tokens
{
    public class AttributeListToken: Token
    {
        public Dictionary<string, string> Attributes { get; set; }
    }
}