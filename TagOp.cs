using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicParser
{
    class TagOp
    {
       
        string TagName;
        Boolean IsTerminal;
        public TagOp(string tagName, Boolean isTerminal)
        {
            this.TagName = tagName;
            this.IsTerminal = isTerminal;
            
        }
    }
}
