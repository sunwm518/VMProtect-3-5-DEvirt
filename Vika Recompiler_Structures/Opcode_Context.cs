using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures
{
   public class Opcode_Context
    {
        public Opcode_Context (IOpCode code, MethodDef meth)
        {
            vika_opcode = code;
            vmp_method_opcode = meth;
        }
        public IOpCode vika_opcode {get; set;}
        public MethodDef vmp_method_opcode { get; set; }

    }
}
