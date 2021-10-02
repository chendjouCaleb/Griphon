using System.Collections.Generic;

namespace Griphon.Structure.Elements
{
    public class DocumentMetaData
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        
        public string Lang { get; set; }

        public string Author { get; set; }

        public string Subject { get; set; }

        public List<string> Keywords { get; set; } = new List<string>();

        public string Date { get; set; }

        public string Publisher { get; set; }
    }
}