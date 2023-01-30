using de4dot.blocks;
using de4dot.blocks.cflow;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Interfaces;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Code_Cleanup
{
    class Post_Clean : ICleaner
    {
        public override void Run(Vika_Context vika)
        {
            foreach (var method in vika.devirt_context.disassembled_mds)
            {
                //try
                //{
                ////    BlocksCflowDeobfuscator deobfuscator = new BlocksCflowDeobfuscator();
                ////    deobfuscator.Add(new MethodCallInliner(true)); //Added this for my own benefit, you can remove it yourself
                ////    Blocks blocks = new Blocks(method.method);
                ////    IList<Instruction> allInstructions;
                ////    IList<ExceptionHandler> allExceptionHandlers2;
                ////    blocks.GetCode(out allInstructions, out allExceptionHandlers2);
                ////    deobfuscator.Initialize(blocks);
                ////    deobfuscator.Deobfuscate();

                ////    IList<ExceptionHandler> allExceptionHandlers;
                ////    blocks.GetCode(out allInstructions, out allExceptionHandlers);
                ////    DotNetUtils.RestoreBody(method.method, allInstructions, allExceptionHandlers2);
                ////}
                //catch
                //{

                //}
            }
        }
        public static int stage_number() { return 4; }

        public override string who()
        {
            throw new NotImplementedException();
        }
    }
}
