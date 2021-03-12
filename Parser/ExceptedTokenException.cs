using System;
using System.Collections.Generic;
using System.Text;

namespace Griphon.Parser
{
    public class ExceptedTokenException: GriphonException
    {
        public ExceptedTokenException(string message): base("EXPECTED_TOKEN_ERROR", message)
        {

        }

        public ExceptedTokenException(string code, string message) : base(code, message)
        {

        }
    }
}
