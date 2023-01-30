using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.Arrays
{
    class LdelemRef : IOpCode
    {
        public override bool is_opcode(MethodDef md)
        {
            List<OpCode> codes_orig = new List<OpCode>();
            md.Body.SimplifyMacros(null);
            foreach (var instr in md.Body.Instructions)
            {
                codes_orig.Add(instr.OpCode);
            }
            bool flag = true;
            if (codes_orig.Count > pattern.Count)
            {
                for (int i = 0; i < codes_orig.Count; i++)
                {
                    try
                    {
                        if (codes_orig[i] != pattern[i]) return false;
                    }
                    catch
                    {

                    }
                }
            }
            else
            {
                return false;
            }

            if (md.Body.Instructions[6].OpCode == OpCodes.Isinst) return true; else return false;




        }


        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            
            return new Vika_Instruction(OpCodes.Ldelem_Ref);
        }
        public override bool nop_before()
        {
            return false;
        }

        public override int Op_Size()
        {
            return 0;
        }
        public override bool is_specially()
        {
            return false;
        }
        List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
            OpCodes.Call,
            OpCodes.Stloc,
            OpCodes.Ldarg,
        };
       
    }
}
