using de4dot.blocks;
using de4dot.blocks.cflow;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Recompiler
{
    class Restore_Handlers : IDevirtStage
    {
        public override void Run(Devirt_Context ctx, Vika_Context vika)
        {
            foreach(var method in ctx.disassembled_mds)
            {
                

                foreach (var ehs in method.handlers)
                    {
                    try
                    {
                        switch (ehs.eh_type)
                        {
                            case 0:
                                {
                                    var catch_type = (ITypeDefOrRef)vika.module.ResolveToken(ehs.token_catch);
                                    var try_block = method.blocks.Single(block => block.instrs.Exists(instr => instr.offset == ehs.try_offset));
                                    var hand_start_block = method.blocks.Single(block => block.instrs.Exists(instr => instr.offset == ehs.handler_offset));
                                    var hand_end_block = method.blocks.Single(block => block.instrs.Exists(instr => instr.offset == ehs.handler_end));
                                    var try_instr = try_block.instrs.Single(instr => instr.offset == ehs.try_offset).Instruction;
                                    var handstart = hand_start_block.instrs.Single(instr => instr.offset == ehs.handler_offset).Instruction;
                                    var handend = hand_end_block.instrs.Single(instr => instr.offset == ehs.handler_end).Instruction;
                            

                                    method.method.Body.ExceptionHandlers.Add(new ExceptionHandler() { CatchType = catch_type, TryStart = try_instr, TryEnd = handstart, HandlerStart = handstart, HandlerEnd = handend, HandlerType = ExceptionHandlerType.Catch, FilterStart = null });
                                    break;
                                }
                            case 2:
                                {



                                    var catch_type = (ITypeDefOrRef)vika.module.ResolveToken(ehs.token_catch);
                                    var try_block = method.blocks.Single(block => block.instrs.Exists(instr => instr.offset == ehs.try_offset));
                                    var hand_start_block = method.blocks.Single(block => block.instrs.Exists(instr => instr.offset == ehs.handler_offset));
                                    var hand_end_block = method.blocks.Single(block => block.instrs.Exists(instr => instr.offset == ehs.handler_end));
                                    var try_instr = method.method.Body.Instructions[method.method.Body.Instructions.IndexOf(try_block.instrs.Single(instr => instr.offset == ehs.try_offset).Instruction)];
                                    var handstart = method.method.Body.Instructions[method.method.Body.Instructions.IndexOf(hand_start_block.instrs.Single(instr => instr.offset == ehs.handler_offset).Instruction)];
                                    var handend = method.method.Body.Instructions[method.method.Body.Instructions.IndexOf(hand_end_block.instrs.Single(instr => instr.offset == ehs.handler_end).Instruction)];
                                 
                                    method.method.Body.ExceptionHandlers.Add(new ExceptionHandler() { CatchType = catch_type, TryStart = try_instr, TryEnd = handstart, HandlerStart = handstart, HandlerEnd = handend, HandlerType = ExceptionHandlerType.Finally, FilterStart = null });


                                    break;
                                }
                            default: throw new Exception(ehs.eh_type.ToString());
                        }
                    }
                    catch
                    {

                    }
                  
                    
                    }
               
                }
            }
        

        public override string who()
        {
            throw new NotImplementedException();
        }
        public static int stage_number() { return 8; }
    }
}
