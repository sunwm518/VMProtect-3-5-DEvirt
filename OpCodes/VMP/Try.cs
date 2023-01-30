using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.VMP
{
    class Try : IOpCode
    {
        public override bool is_opcode(MethodDef md)
        {
            List<OpCode> codes_orig = new List<OpCode>();
            md.Body.SimplifyMacros(null);
            foreach (var instr in md.Body.Instructions)
            {
                codes_orig.Add(instr.OpCode);
            }
          
            try
            {
                if (md.Body.Instructions[md.Body.Instructions.Count - 2].OpCode == OpCodes.Endfinally) return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        public override int Op_Size()
        {
            return 0;
        }
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {

        

            return new Vika_Instruction(OpCodes.Nop);
        }

        List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
           OpCodes.Call,
            OpCodes.Callvirt,
           OpCodes.Stloc,

             OpCodes.Ldarg,
           OpCodes.Ldfld,
            OpCodes.Callvirt,
           OpCodes.Stloc,

        


        };
        public override bool nop_before()
        {
            return false;
        }

        public override bool is_specially()
        {
            return true;
        }
    }
}
