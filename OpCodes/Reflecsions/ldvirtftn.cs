using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.Reflecsions
{
    class ldvirtftn : IOpCode
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
            var kk = md.Body.Instructions.Where(sd => sd.OpCode == OpCodes.Callvirt);
            if (kk.Count()>0)
            {
                if (kk.ToList().Exists(sd => sd.Operand.ToString().Contains("GetBaseDefinition")))
                {
                   
                        return true;
                    
                }


            }




            return false;


        }

        public override int Op_Size()
        {
            return 4;
        }
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {

            var token = vika.module.ResolveToken(read.ReadUInt32());
            var alls = (IMethodDefOrRef)token;
        
                return new Vika_Instruction(OpCodes.Ldvirtftn, alls);
        }

  
    }
}
