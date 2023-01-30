using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.Values
{
    class Ldstr : IOpCode
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
                for (int i = 0; i == codes_orig.Count; i++)
                {
                    if (codes_orig[i] != pattern[i]) return false;
                }
            }
            else
            {
                return false;
            }

            try
            {
                var add_oper = md.Body.Instructions[5].Operand as MethodDef;
                if (add_oper.ReturnType.ElementType == ElementType.String)
                    return true;
                else
                {
                    return false
                        ;
                }
            }
            catch
            {

                return false;
            }
        }
        public override bool nop_before()
        {
            return true;
        }

        public override bool is_specially()
        {
            return false;
        }

        public override int Op_Size()
        {
            return 0;
        }
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            var str = vika.module.ReadUserString(Convert.ToUInt32(vika.value_stack.Pop()));
          //  Console.WriteLine(str);

            return new Vika_Instruction(OpCodes.Ldstr,str);
        }
        List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
            OpCodes.Ldarg,
            OpCodes.Ldarg,
            OpCodes.Call,
            OpCodes.Callvirt,
            OpCodes.Call,
            OpCodes.Newobj,
            OpCodes.Call,
            OpCodes.Ret,


        };


    }
}
