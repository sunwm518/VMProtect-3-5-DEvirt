using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Before_Devirt.VMP_Finder
{
    class Find_Opcodes_Methods : IPreDevirtStage
    {
        public override void Run(Pre_Devirt_Context ctx, ModuleDefMD mod)
        {
            var initer = ctx.vmp_type.FindDefaultConstructor();
            ctx.logger.debug("Found VMP Initer Method: " + initer.FullName);
            var instr_opcodes = initer.Body.Instructions.Where(instr => instr.OpCode == OpCodes.Ldftn).ToList();
            ctx.logger.debug("Found " + instr_opcodes.Count + " VMP OpCodes");
            ctx.vmp_opcodes_methods = instr_opcodes.Select(instr => instr.Operand as MethodDef).Distinct().ToList();


        }

        public override string who()
        {
            throw new NotImplementedException();
        }
        public static int stage_number() { return 2; }

    }
}
