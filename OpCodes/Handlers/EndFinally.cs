using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using System.Collections.Generic;
using System.IO;

namespace Vika_Anti_VMP_OOP_3._5_Based.Logicals
{
    class EndFinally : IOpCode
    {
        public override bool is_opcode(MethodDef md)
        {
            List<OpCode> codes_orig = new List<OpCode>();
            md.Body.SimplifyMacros(null);
            foreach (var instr in md.Body.Instructions)
            {
                codes_orig.Add(instr.OpCode);
            }
            if (codes_orig.Count > pattern.Count)
            {
                try
                {
                    for (int i = 0; i < codes_orig.Count; i++)
                    {
                        if (codes_orig[i] != pattern[i]) return false;
                    }
                }
                catch
                {

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
            return 0;
        }
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {

            return new Vika_Instruction(OpCodes.Endfinally);
        }

        List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
           OpCodes.Ldfld,
            OpCodes.Brtrue,
           OpCodes.Ldarg,

             OpCodes.Ldarg,
           OpCodes.Ldfld,
            OpCodes.Callvirt,
           OpCodes.Stfld,

            OpCodes.Ret,



        };
        public override bool nop_before()
        {
            return false;
        }

        public override bool is_specially()
        {
            return false;
        }
    }
}
