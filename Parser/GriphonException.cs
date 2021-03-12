using System;
using System.Collections.Generic;
using System.Text;

namespace Griphon.Parser
{
    public class GriphonException: ApplicationException
    {
        public string Code { get; set; }

        public GriphonException(string code, string message): base(message)
        {
            this.Code = code;
        }
    }
}
