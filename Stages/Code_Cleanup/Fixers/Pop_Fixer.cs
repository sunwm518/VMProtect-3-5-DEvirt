using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Interfaces;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Code_Cleanup
{
    class Pop_Fixer : ICleaner
    {
        public override void Run(Vika_Context vika)
        {
            foreach(var method in vika.devirt_context.disassembled_mds)
            {
                var pop_instr = method.method.Body.Instructions.Where(opc => opc.OpCode == OpCodes.Pop).ToList();
                if (pop_instr.Count > 0)
                {
                    pop_instr[0].OpCode = OpCodes.Nop;
                }
            }
        }
        public static int stage_number() { return 1; }

        public override string who()
        {
            throw new NotImplementedException();
        }
    }
}
