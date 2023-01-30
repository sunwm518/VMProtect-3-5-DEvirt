using de4dot.blocks;
using de4dot.blocks.cflow;
using DeMutation;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Collections.Generic;

namespace Vika_Anti_VMP.Helpers
{
    public class Mutation_Cleaner
    {
        public static void clean(MethodDef method)
        {
            try
            {
                method.Body.SimplifyMacros(method.Parameters);
            }
            catch
            {
                try
                {
                    method.Body.SimplifyMacros(null);
                }
                catch
                {

                }

            }
            try
            {
                BlocksCflowDeobfuscator deobfuscator = new BlocksCflowDeobfuscator();
                deobfuscator.Add(new ControlFlowDeobfuscator());
                deobfuscator.Add(new MethodCallInliner(true)); //Added this for my own benefit, you can remove it yourself
                Blocks blocks = new Blocks(method);
                deobfuscator.Initialize(blocks);
                deobfuscator.Deobfuscate();
                blocks.RemoveDeadBlocks();
                blocks.RepartitionBlocks();
                blocks.UpdateBlocks();
                deobfuscator.Deobfuscate();
                blocks.RepartitionBlocks();
                deobfuscator.Deobfuscate();
                blocks.RemoveDeadBlocks();
                blocks.RepartitionBlocks();
                blocks.UpdateBlocks();
                deobfuscator.Deobfuscate();
                blocks.RepartitionBlocks();
                IList<Instruction> allInstructions;
                IList<ExceptionHandler> allExceptionHandlers;
                blocks.GetCode(out allInstructions, out allExceptionHandlers);
                DotNetUtils.RestoreBody(method, allInstructions, allExceptionHandlers);
            }
            catch
            {

            }
        }
    }
}
