using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt
{
   public class Devirt_Context
    {
        internal List<Vika_Method> disassembled_mds;

        public List<int> offsets_list { get; internal set; }
        public VikaStack offsets { get; internal set; }
        public Vika_Context vika { get; set; }
    }
}
