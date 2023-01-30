using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Before_Devirt
{
   public class Pre_Devirt_Context
    {
        public TypeDef vmp_type { get; set; }
        public ILogger logger { get; set; }
        public List<MethodDef> vmp_opcodes_methods { get; set; }

        public List<Opcode_Context> all_opcodes { get; set; }
        public Dictionary<int, Opcode_Context> structured_all_opcodes { get; set; }
        public Dictionary<int,MethodDef> virt_methods { get; set; }


    }
}
