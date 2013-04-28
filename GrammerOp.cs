using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicParser
{
    class GrammerOp
    {
        public string grammer;
        public string LeftSide;
        public string RightSide;
        List<string> MyTags;
        public Boolean IsMaster;
        public GrammerOp(string Mygrammer, Boolean isMaster)
        {
            this.IsMaster = isMaster;
            RightSide = "";
            MyTags = new List<string>();
            if(GetRightAndLeftSide(Mygrammer))
            {

            }
            this.grammer = Mygrammer;
        }
        Boolean GetRightAndLeftSide(string Mygrammer)
        {
            string[] Tags = Mygrammer.Split(' ');
            LeftSide = Tags[0];
            int Index = 0;
            foreach (string tag in Tags)
            {
                if (tag != "=" && tag != "+" && Index != 0)
                {
                    RightSide = RightSide +  tag + " ";
                    MyTags.Add(tag);
                }
                Index++;
            }
            /*
            int Index = 0;
            foreach(char Liter in Mygrammer)
            {
                if (Liter == '=')
                {

                }
                Index++;
            }*/
            return false;
        }

    }
}
