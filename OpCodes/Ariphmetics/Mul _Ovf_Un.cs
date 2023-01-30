using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using Vika_Anti_VMP.Helpers;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Ariphmetics
{
    class Mul_Ovf_Un : IOpCode
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
            if (md.Body.Instructions[10].GetLdcI4Value() == 1 && md.Body.Instructions[11].GetLdcI4Value() == 1)
            {
                var add_oper = md.Body.Instructions[12].Operand as MethodDef;

                if (add_oper.Body.Variables.Count > 13)
                {

                    Mutation_Cleaner.clean(add_oper);
                    foreach (var instr in add_oper.Body.Instructions)
                    {
                        if (instr.OpCode == OpCodes.Add)
                        {

                            return false;
                        }
                    }
                    bool flag = false;
                    foreach (var instr in add_oper.Body.Instructions)
                    {
                        if (instr.OpCode == OpCodes.Mul)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        foreach (var instr in add_oper.Body.Instructions)
                        {
                            if (instr.OpCode == OpCodes.Mul_Ovf)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        List<OpCode> pattern = new List<OpCode>() {
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
                        OpCodes.Ldc_I4,

            OpCodes.Call,
            OpCodes.Call,
            OpCodes.Ret

        };
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            return new Vika_Instruction(OpCodes.Mul_Ovf_Un);
        }

        public override bool nop_before()
        {
            return false;
        }

        public override bool is_specially()
        {
            return false;
        }

        public override int Op_Size()
        {
            return 0;
        }
    }
}
