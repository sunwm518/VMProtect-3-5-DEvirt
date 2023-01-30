using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;


namespace Vika_Anti_VMP_OOP_3._5_Based.VMP
{
    class Conv : IOpCode
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

            if (md.Body.Instructions[4].Operand.ToString().Contains("Type")) return true; else return false;

            return false;
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
                
                case ElementType.I:   return new Vika_Instruction(OpCodes.Conv_I);
                case ElementType.Boolean: return new Vika_Instruction(OpCodes.Conv_I);

                case ElementType.I1: return new Vika_Instruction(OpCodes.Conv_I1);
                case ElementType.I2: return new Vika_Instruction(OpCodes.Conv_I2);
                case ElementType.I4: return new Vika_Instruction(OpCodes.Conv_I4);
                case ElementType.I8: return new Vika_Instruction(OpCodes.Conv_I8);
                case ElementType.R4: return new Vika_Instruction(OpCodes.Conv_R4);
                case ElementType.R8: return new Vika_Instruction(OpCodes.Conv_R8);
                case ElementType.U: return new Vika_Instruction(OpCodes.Conv_U);
                case ElementType.U1: return new Vika_Instruction(OpCodes.Conv_U1);
                case ElementType.U2: return new Vika_Instruction(OpCodes.Conv_U2);
                case ElementType.U4: return new Vika_Instruction(OpCodes.Conv_U4);
                case ElementType.U8: return new Vika_Instruction(OpCodes.Conv_U8);

                default:Console.WriteLine(t.ToTypeSig().ElementType); throw new Exception();
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
           OpCodes.Ldarg,

               OpCodes.Ldarg,
           OpCodes.Call,
            OpCodes.Ldloc,
           OpCodes.Call,

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
