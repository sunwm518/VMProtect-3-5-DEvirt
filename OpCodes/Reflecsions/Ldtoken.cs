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
    class Ldtoken : IOpCode
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
           
            if (md.Body.Variables.Count ==4)
            {
                if (md.Body.Variables.ToList().Exists(sd=>sd.Type.FullName.Contains( "Handle"))) return true;
            }
            else
            {
                return false;
            }



            return false;

        }

        public override int Op_Size()
        {
            return 0;
        }
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            uint token = Convert.ToUInt32(vika.value_stack.Pop());
            var tok_res = vika.module.ResolveToken(Convert.ToUInt32(token));
            int num2 = (int)token >> 24;
            if (num2 <= 10)
            {
                switch (num2)
                {
                    case 10:
                        try
                        {
                            return new Vika_Instruction(OpCodes.Ldtoken, (IField)tok_res);
                        }
                        catch
                        {
                            return new Vika_Instruction(OpCodes.Ldtoken, (IMethodDefOrRef)tok_res);
                        }
                    case 4:
                        return new Vika_Instruction(OpCodes.Ldtoken, (IField)tok_res);
                    case 6:
                        goto IL_0112;
                    case 1:
                    case 2:
                        goto IL_0145;
                }
            }
            else
            {
                if (num2 == -673208261)
                {
                    goto IL_0145;
                }
                if (num2 == 4192395)
                {
                    goto IL_0112;
                }
            }
            throw new InvalidOperationException();
        IL_0145:
            return new Vika_Instruction(OpCodes.Ldtoken, (ITypeDefOrRef)tok_res);
        IL_0112:
            return new Vika_Instruction(OpCodes.Ldtoken, (IMethodDefOrRef)tok_res);


        }

   
    }
}
