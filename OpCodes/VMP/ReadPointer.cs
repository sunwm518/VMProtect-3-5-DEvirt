
    using dnlib.DotNet;
    using dnlib.DotNet.Emit;
    using dnlib.PE;
    using System;
    using System.Collections.Generic;
    using System.IO;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.VMP
    {
        class ReadPointer : IOpCode
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
                return 8;
            }
            public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
            {
                int offset = Convert.ToInt32(vika.value_stack.Pop());
                var reader = new BinaryReader(new MemoryStream(vika.module.Metadata.PEImage.CreateReader(vika.module.Metadata.PEImage.ToFileOffset((RVA)offset)).ToArray()));

                vika.value_stack.Push(reader.ReadInt32());
                var reader2 = new BinaryReader(new MemoryStream(vika.module.Metadata.PEImage.CreateReader(vika.module.Metadata.PEImage.ToFileOffset((RVA)offset + 4)).ToArray()));
                vika.value_stack.Push(reader2.ReadInt32());

                return new Vika_Instruction(OpCodes.Nop);
            }

            List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
           OpCodes.Ldarg,
            OpCodes.Ldfld,
           OpCodes.Ldarg,

             OpCodes.Call,
           OpCodes.Callvirt,
            OpCodes.Conv_U8,
           OpCodes.Add,

               OpCodes.Newobj,
           OpCodes.Call,

           OpCodes.Newobj,
            OpCodes.Call,
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

