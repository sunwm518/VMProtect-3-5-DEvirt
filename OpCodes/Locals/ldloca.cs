using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.Locals
{
    class ldloca : IOpCode
    {
        public override bool is_opcode(MethodDef md)
        {
            List<OpCode> codes_orig = new List<OpCode>();
            md.Body.SimplifyMacros(null);
            foreach (var instr in md.Body.Instructions)
            {
                codes_orig.Add(instr.OpCode);
            }
            if (codes_orig.Count == pattern.Count)
            {
                for (int i = 0; i < codes_orig.Count; i++)
                {
                    if (codes_orig[i] != pattern[i]) return false;
                }
            }
            else
            {
                return false;
            }



            return true;
        }


        public override int Op_Size()
        {
            return 2;
        }
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            var index = read.ReadInt16();
        
                return new Vika_Instruction(OpCodes.Ldloc, md.method.Body.Variables[index]);
      

        }
        public override bool nop_before()
        {
            return false;
        }

        public override bool is_specially()
        {
            return false;
        }
        List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
            OpCodes.Ldarg,
            OpCodes.Ldfld,
            OpCodes.Ldarg,

            OpCodes.Call,
            OpCodes.Conv_U2,
            OpCodes.Callvirt,
            OpCodes.Newobj,

            OpCodes.Call,
            OpCodes.Ret,


        };
    }
}
