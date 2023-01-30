using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Before_Devirt.Virt_Methods_Finder
{
    class Find_Virt_Methods : IPreDevirtStage
    {
        public override void Run(Pre_Devirt_Context ctx, ModuleDefMD mod)
        {
            ctx.virt_methods = new Dictionary<int, MethodDef>();
            var all_methods = mod.GetTypes().SelectMany(t => t.Methods).ToList();
            var targets_all = all_methods.Where(meth => meth.HasBody && meth.DeclaringType != ctx.vmp_type && meth.Body.Instructions.Where(instr => instr.OpCode == OpCodes.Newobj).Count() == 1);
            var targets_finish = targets_all.Where(method => (method.Body.Instructions.Single(instr => instr.OpCode == OpCodes.Newobj).Operand as IMethod).DeclaringType == ctx.vmp_type);
            ctx.logger.info("Found " + targets_finish.Count() + " Virtualised methods");
            foreach(var metod_virt in targets_finish)
            {
                int position = metod_virt.Body.Instructions.IndexOf(metod_virt.Body.Instructions.Single(instr => instr.OpCode == OpCodes.Newobj))+2;
                int offset = metod_virt.Body.Instructions[position].GetLdcI4Value();
                ctx.logger.debug("Virt Method: " + metod_virt.Name + " at offset: " + offset);
                if(!ctx.virt_methods.ContainsKey(offset))
                ctx.virt_methods.Add(offset, metod_virt);
            }
        }

        public override string who()
        {
            throw new NotImplementedException();
        }
        public static int stage_number() { return 5; }

    }
}
