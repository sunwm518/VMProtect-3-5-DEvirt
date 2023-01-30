using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using System.IO;
using Vika_Anti_VMP.Helpers;

namespace Vika_Anti_VMP_OOP_3._5_Based.Values
{
    class LdcI8 : IOpCode
    {
        public override bool is_opcode(MethodDef md)
        {
            List<OpCode> codes_orig = new List<OpCode>();
            md.Body.SimplifyMacros(null);
            foreach (var instr in md.Body.Instructions)
            {
                codes_orig.Add(instr.OpCode);
            }
            bool old = true;
            if (codes_orig.Count == pattern.Count)
            {
                for (int i = 0; i < codes_orig.Count; i++)
                {
                    if (codes_orig[i] != pattern[i]) old = false;
                }
            }
            else
            {
                old = false;
            }
            if (old)
            {
                var add_oper = md.Body.Instructions[2].Operand as MethodDef;

                Mutation_Cleaner.clean(add_oper);
                foreach (var instr in add_oper.Body.Instructions)
                {
                    if (instr.OpCode == OpCodes.Call)
                    {
                        if (instr.Operand.ToString().Contains("ReadInt64"))
                            return true;
                    }
                }
            }
            else
            {
                if (codes_orig.Count == pattern_36x.Count)
                {
                    for (int i = 0; i < codes_orig.Count; i++)
                    {
                        if (codes_orig[i] != pattern_36x[i]) return false;
                    }
                }
                else
                {
                    return false;
                }
                var add_oper = md.Body.Instructions[5].Operand.ToString();
                if (add_oper.Contains("System.Int64"))
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }



            return false;
        }
        List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
           OpCodes.Ldarg,
            OpCodes.Call,
            OpCodes.Newobj,
            OpCodes.Call,
            OpCodes.Ret

        };
        List<OpCode> pattern_36x = new List<OpCode>() {
            OpCodes.Ldarg,
           OpCodes.Ldfld,
            OpCodes.Ldarg,
                        OpCodes.Ldfld,
                                    OpCodes.Callvirt,

            OpCodes.Newobj,
            OpCodes.Callvirt,
            OpCodes.Ret

        };

        public override int Op_Size()
        {
            return 8;
        }

        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            var val = read.ReadInt64();
            return new Vika_Instruction(OpCodes.Ldc_I8, val);
        }
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
