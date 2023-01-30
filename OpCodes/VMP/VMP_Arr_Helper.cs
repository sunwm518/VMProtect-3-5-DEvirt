
    using dnlib.DotNet;
    using dnlib.DotNet.Emit;
    using global::Vika_Anti_VMP_OOP_3._5_Based.Ariphmetics;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

    using System.Linq;

    namespace Vika_Anti_VMP_OOP_3._5_Based.VMP
    {
        class VMP_Arr_Helper : IOpCode
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
                        for (int i = 0; i < pattern.Count; i++)
                        {
                            if (codes_orig[i] != pattern[i]) return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                if (md.Body.Instructions.Last().OpCode == OpCodes.Throw)
                    return true;
                else return false;
            }

            public override int Op_Size()
            {
                return 0;
            }
            public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
            {
                return new Vika_Instruction(OpCodes.Stelem, (ITypeDefOrRef)vika.module.ResolveToken(Convert.ToUInt32(vika.value_stack.Pop())));

            }

            List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
           OpCodes.Ldarg,
            OpCodes.Call,
           OpCodes.Callvirt,

             OpCodes.Call,
           OpCodes.Pop,
            OpCodes.Ldarg,
           OpCodes.Call,

               OpCodes.Pop,
           OpCodes.Br,
         

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

