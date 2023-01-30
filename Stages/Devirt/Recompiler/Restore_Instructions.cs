using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Recompiler
{
    class Restore_Instructions : IDevirtStage
    {
        public override void Run(Devirt_Context ctx, Vika_Context vika)
        {
            for (int i = 0; i < ctx.disassembled_mds.Count; i++)
            {
                restore_blocks(ctx.disassembled_mds[i]);
            }
        }
        public static void restore_blocks(Vika_Method method)
        {
            var md =method.method;
            md.Body.Instructions.Clear();
            foreach (var bl in method.blocks)
            {
             //   if (bl.bl_type == Vika_BL_TYPE.none) continue;
                foreach (var instrs in bl.instrs)
                {
                    md.Body.Instructions.Add(instrs.Instruction);
                }

            }
            md.Body.UpdateInstructionOffsets();
            md.Body.OptimizeBranches();
        }
        public override string who()
        {
            throw new NotImplementedException();
        }
        public static int stage_number() { return 6; }

    }
}
