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
    class Setup_EH_Handlers : IOpCode
    {

        public override int Op_Size()
        {
            return 17;
        }
        public override bool is_opcode(MethodDef md)
        {
            if (md.Body.Variables.Count == 12 && md.Body.Instructions.Count>20)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            Vika_Handler eshka = new Vika_Handler
            {
                eh_type = read.ReadByte(),
                try_len = read.ReadInt32(),
                unknown_offset_ehs = read.ReadInt32(),
                handler_offset = read.ReadInt32(),
                token_catch = read.ReadInt32(),
                handler_end = 0
            };
            md.handlers.Add(eshka);

            return new Vika_Instruction(OpCodes.Nop, null);
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
