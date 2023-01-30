
    using dnlib.DotNet;
    using dnlib.DotNet.Emit;
    using global::Vika_Anti_VMP_OOP_3._5_Based.Ariphmetics;
    using System;
    using System.Collections.Generic;
    using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

    using System.IO;

    namespace Vika_Anti_VMP_OOP_3._5_Based.VMP
    {
        class Stind : IOpCode
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


                return true;
            }

            public override int Op_Size()
            {
                return 0;
            }
         
             public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            var t = (ITypeDefOrRef)vika.module.ResolveToken(Convert.ToUInt32(vika.value_stack.Pop()));
            switch (t.ToTypeSig().ElementType)
            {

                case ElementType.I: return new Vika_Instruction(OpCodes.Stind_I);
                case ElementType.Boolean: return new Vika_Instruction(OpCodes.Stind_I);

                case ElementType.I1: return new Vika_Instruction(OpCodes.Stind_I1);
                case ElementType.I2: return new Vika_Instruction(OpCodes.Stind_I2);
                case ElementType.I4: return new Vika_Instruction(OpCodes.Stind_I4);
                case ElementType.I8: return new Vika_Instruction(OpCodes.Stind_I8);
                case ElementType.R4: return new Vika_Instruction(OpCodes.Stind_R4);
                case ElementType.R8: return new Vika_Instruction(OpCodes.Stind_R8);
                case ElementType.ByRef: return new Vika_Instruction(OpCodes.Stind_Ref);
                case ElementType.Object: return new Vika_Instruction(OpCodes.Stind_Ref);


                default: Console.WriteLine(t.ToTypeSig().ElementType); throw new Exception();
            }

        }

            List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
           OpCodes.Ldarg,
            OpCodes.Call,
           OpCodes.Callvirt,

             OpCodes.Call,
           OpCodes.Stloc,
            OpCodes.Ldarg,
           OpCodes.Call,

               OpCodes.Stloc,
           OpCodes.Ldarg,
           OpCodes.Call,
            OpCodes.Stloc,

               OpCodes.Ldloc,
           OpCodes.Callvirt,
           OpCodes.Brtrue,
            OpCodes.Ldloc,

               OpCodes.Callvirt,
           OpCodes.Isinst,


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

