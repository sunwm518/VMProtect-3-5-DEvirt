using System;
using System.Collections.Generic;
using System.IO;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.Logicals
{
    class IsInst : IOpCode
    {
        public override bool nop_before()
        {
            return false;
        }

        public override bool is_specially()
        {
            return false;
        }
        public override bool is_opcode(MethodDef md)
        {
            List<OpCode> codes_orig = new List<OpCode>();
            md.Body.SimplifyMacros(null);
            foreach (var instr in md.Body.Instructions)
            {
                codes_orig.Add(instr.OpCode);
            }
            if (codes_orig.Count>pattern.Count)
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

            if (md.Body.Instructions.ToList().Exists(sd => sd.OpCode == OpCodes.Ldnull) && md.Body.Instructions.ToList().Where(sd => sd.OpCode == OpCodes.Newobj).Count() == 1)
            {
             
                    return true;


            }
            return false;

        }

        public override int Op_Size()
        {
            return 0;
        }

        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            return new Vika_Instruction(OpCodes.Isinst, (ITypeDefOrRef)vika.module.ResolveToken(vika.value_stack.Pop()));

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

        OpCodes.Ldloc,
            OpCodes.Callvirt,
            OpCodes.Ldloc,
            OpCodes.Call,

        };
    } 
}
