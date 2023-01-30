using System;
using de4dot.blocks;
using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based
{
    public class Vika_Instruction
    {

        public Vika_Instruction(OpCode nop, dynamic p = null)
        {
            this.code = nop;
            this.value = p;
        }

        public Instruction Instruction { get; set; }

        public OpCode code { get; set; }
        public int offset { get; set; }
        public int target { get; set; }
        public dynamic value { get; set; }

        internal void restore()
        {
            if(value != null)
            {
                Instruction = new Instruction(code, value);
            }
            else
            {
                Instruction =new Instruction(code);

            }
        }

        internal void nop()
        {
            Instruction = OpCodes.Nop.ToInstruction();
            code = OpCodes.Nop;
        }
    }
}
