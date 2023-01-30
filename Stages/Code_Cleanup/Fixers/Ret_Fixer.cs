using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Interfaces;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Code_Cleanup
{
    class Ret_Fixer : ICleaner
    {
        public override void Run(Vika_Context vika)
        {
            foreach(var method in vika.devirt_context.disassembled_mds)
            {
                var ret_instr = method.method.Body.Instructions.Where(opc => opc.OpCode == OpCodes.Ret ).ToList();
                if (ret_instr.Count > 0)
                {
                    foreach (var ret in ret_instr)
                    {
                        var instrs = method.method.Body.Instructions;
                        var ret_index = method.method.Body.Instructions.IndexOf(ret);
                    
                        if (instrs[ret_index - 1].OpCode == OpCodes.Nop && instrs[ret_index - 2].OpCode == OpCodes.Ldnull)
                            instrs[ret_index - 2].OpCode = OpCodes.Nop;
                    }
                }
            }
        }
        public static int stage_number() { return 3; }

        public override string who()
        {
            throw new NotImplementedException();
        }
    }
}
