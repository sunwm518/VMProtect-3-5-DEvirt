using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.Reflecsions
{
    class UnboxAny : IOpCode
    {
        public override bool nop_before()
        {
            return true;
        }

        public override bool is_specially()
        {
            return false;
        }
        public override bool is_opcode(MethodDef md)
        {
            List<OpCode> codes_orig = new List<OpCode>();
            md.Body.SimplifyMacros(null);

            var objs = md.Body.Instructions.Where(instr => instr.OpCode == OpCodes.Newobj).ToList();
            bool dd = false;
            bool gg = false;
            if (objs.ToList().Count > 0)
            {
                foreach (var obj in objs)
                {
                    if (obj.Operand.ToString().Contains("Cast") )dd = true;
                 if (obj.Operand.ToString().Contains("NullReferenceException")) gg= true;
                 
                }
            }


            return gg&&dd;
        }

        public override int Op_Size()
        {
            return 0;
        }
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            uint token = Convert.ToUInt32(vika.value_stack.Pop());
            return new Vika_Instruction(OpCodes.Unbox_Any, (ITypeDefOrRef)vika.module.ResolveToken(Convert.ToUInt32(token)));
        }

    }
}
