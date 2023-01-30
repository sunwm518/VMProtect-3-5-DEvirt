using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Before_Devirt.VMP_Finder
{
   public class Find_Vmp_Type : IPreDevirtStage
    {
        public override void Run(Pre_Devirt_Context ctx, ModuleDefMD module)
        {
            var types = module.Types.Where(type => !type.IsGlobalModuleType).ToList();
            foreach(var type in types)
            {
                var methods = type.Methods.Where(method => method.HasBody && method.IsConstructor && !method.IsStaticConstructor).ToList().Where(meth=>meth.Body.Instructions.Where(instr => instr.OpCode == OpCodes.Ldftn).Count() > 100).ToList();

               if(methods.Count() > 0)
                {
                    ctx.vmp_type = methods[0].DeclaringType;
                }
            }
            if (ctx.vmp_type != null)
            {
                ctx.logger.debug("Found VMP TYPE: " + ctx.vmp_type.FullName);

            }
            else
            {
                throw new Exception("Not Found VMP TYPE");
            }
        }
        public static int stage_number() { return 1; }

        public override string who()
        {
            return "Просто ищет тип вмп с вм";
        }
    }
}
