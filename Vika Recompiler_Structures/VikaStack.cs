using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures
{
    public class VikaStack
    {
        public List<int> offsets = new List<int>();
        public int pop()
        {
            var t = offsets.Last();
            offsets.Remove(t);
            return t;
        }
        public void push(int t)
        {
 
            offsets.Add(t);
        }
        public void push_end(int t)
        {

            offsets.Insert(0,t);
        }
    }
}
