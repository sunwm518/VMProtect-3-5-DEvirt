using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.Ariphmetics
{
    class convi4 : IOpCode
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


            if (md.Body.Instructions[14].GetLdcI4Value() == 1) return true;
            return false;
        }
        public override bool nop_before()
        {
            return true;
        }

        public override bool is_specially()
        {
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
           OpCodes.Call,
            OpCodes.Stloc,
            OpCodes.Ldarg,

                 OpCodes.Call,
           OpCodes.Stloc,
            OpCodes.Ldarg,
            OpCodes.Ldarg,

                 OpCodes.Ldloc,
           OpCodes.Ldloc,
            OpCodes.Ldc_I4,
            OpCodes.Ldloc,

                 OpCodes.Call,
            OpCodes.Newobj,
            OpCodes.Call,





            OpCodes.Ret

        };


    }
}
