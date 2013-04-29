using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ArabicParser
{
    
    class Way
    {
        public List<int> Steps;
        public Way()
        {
            Steps = new List<int>();
        }
        public void AddStep(int Step)
        {
            Steps.Add(Step);
        }
        public Boolean IsEqual(Way OtherWay)
        {
            if (this.Steps.Count != OtherWay.Steps.Count)
            {
                return false;
            }
            int index = 0;
            foreach (int step in this.Steps)
            {
                if (step != OtherWay.Steps[index])
                {
                    return false;
                }
                index++;
            }
            return true;
        }
    }
}
