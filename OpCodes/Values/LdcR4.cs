using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.Values
{
    class LdcR4 : IOpCode
    {
        public override bool is_opcode(MethodDef md)
        {
            List<OpCode> codes_orig = new List<OpCode>();
            md.Body.SimplifyMacros(null);
           foreach(var instr in md.Body.Instructions)
            {
                codes_orig.Add(instr.OpCode);
            }
            if (codes_orig.Count == pattern.Count)
            {
              for(int i = 0; i < codes_orig.Count; i++)
                {
                    if (codes_orig[i] != pattern[i]) return false;
                }
            }
            else
            {
                return false;
            }
         
                var add_oper = md.Body.Instructions[2].Operand as MethodDef;
              if(add_oper.ReturnType.ElementType == ElementType.R4)
                        return true;
                   
            
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

        public override int Op_Size()
        {
            return 4;
        }

        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
           var val = BitConverter.ToSingle(BitConverter.GetBytes(read.ReadInt32()), 0);
            return new Vika_Instruction(OpCodes.Ldc_R4, val);
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
