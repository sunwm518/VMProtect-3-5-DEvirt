using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Vika_Anti_VMP.Helpers;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Before_Devirt.OpCodes_Sorter
{
    class Keys_Finder : IPreDevirtStage
    {
        public override void Run(Pre_Devirt_Context ctx, ModuleDefMD mod)
        {
            Mutation_Cleaner.clean(ctx.vmp_type.FindDefaultConstructor());
            ctx.vmp_type.FindDefaultConstructor().Body.SimplifyMacros(null);
            var instrs = ctx.vmp_type.FindDefaultConstructor().Body.Instructions;
            ctx.structured_all_opcodes = new Dictionary<int, Opcode_Context>();
            int count = 0;
            for (int i = 0; i < instrs.Count; i++)
            {
                if (instrs[i].OpCode == OpCodes.Ldftn)
                {
                    count++;
                    if (instrs[i - 2].IsLdcI4())
                    {
                        if (ctx.all_opcodes.Where(kk => kk.vmp_method_opcode == instrs[i].Operand as MethodDef).Count() > 0)
                        {
                            ctx.logger.info(instrs[i - 2].GetLdcI4Value().ToString() + "\t" + ctx.all_opcodes.Where(kk => kk.vmp_method_opcode == instrs[i].Operand as MethodDef).ToList()[0].vika_opcode.GetType().Name.ToString());

                            ctx.structured_all_opcodes.Add(instrs[i - 2].GetLdcI4Value(), ctx.all_opcodes.Where(kk => kk.vmp_method_opcode == instrs[i].Operand as MethodDef).ToList()[0]);
                        }

                    }
                }
            }
            ctx.logger.info("Total: " + ctx.structured_all_opcodes.Count + "/" + count + " OpCodes");

            ctx.logger.info("Structured: " + ctx.structured_all_opcodes.Count + " OpCodes");
            ctx.vmp_type.Module.Write("renameed.exe", new dnlib.DotNet.Writer.ModuleWriterOptions(ctx.vmp_type.Module) { Logger = DummyLogger.NoThrowInstance });
        }

        public static int stage_number() { return 4; }

        public override string who()
        {
            throw new NotImplementedException();
        }
    }
}
