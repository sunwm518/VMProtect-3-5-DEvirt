using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using System.IO;

namespace Vika_Anti_VMP_OOP_3._5_Based.Logicals
{
    class sizeOf : IOpCode
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
            return 0;
        }
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            uint token = Convert.ToUInt32(vika.value_stack.Pop());
            var tok_res = vika.module.ResolveToken(Convert.ToUInt32(token));
            return new Vika_Instruction(OpCodes.Sizeof, (ITypeDefOrRef)tok_res);
        }

        List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
           OpCodes.Ldarg,
            OpCodes.Ldarg,
           OpCodes.Call,

             OpCodes.Call,
           OpCodes.Call,
            OpCodes.Newobj,
           OpCodes.Call,

            OpCodes.Ret,



        };
        public override bool nop_before()
        {
            return true;
        }

        public override bool is_specially()
        {
            return false;
        }
    }
}
