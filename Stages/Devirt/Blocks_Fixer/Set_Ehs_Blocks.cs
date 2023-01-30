using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Blocks_Sorter
{
    class Set_Ehs : IDevirtStage
    {
        public override void Run(Devirt_Context ctx, Vika_Context vika)
        {
           foreach(var method in ctx.disassembled_mds)
            {
                x:
                foreach(var ehs in method.handlers)
                {
                    try
                    {
                        var try_block = method.blocks.Single(block => block.instrs.Exists(instr => instr.offset == ehs.try_offset));
                        var hand_start_block = method.blocks.Single(block => block.instrs.Exists(instr => instr.offset == ehs.handler_offset));
                        var hand_end_block = method.blocks.Single(block => block.instrs.Exists(instr => instr.offset == ehs.handler_end));
                        Console.WriteLine(hand_start_block.offset);
                        Console.WriteLine(hand_end_block.offset);

                        if (method.blocks.IndexOf(try_block) > method.blocks.IndexOf(hand_start_block))
                        {
                            var one = method.blocks[method.blocks.IndexOf(try_block)];
                            var two = method.blocks[method.blocks.IndexOf(hand_start_block)];
                            method.blocks[method.blocks.IndexOf(try_block)] = two;
                            method.blocks[method.blocks.IndexOf(hand_start_block)] = one;

                        }
                        if (method.blocks.IndexOf(hand_start_block) > method.blocks.IndexOf(hand_end_block))
                        {
                            var one = method.blocks[method.blocks.IndexOf(hand_start_block)];
                            var two = method.blocks[method.blocks.IndexOf(hand_end_block)];
                            method.blocks[method.blocks.IndexOf(hand_start_block)] = two;
                            method.blocks[method.blocks.IndexOf(hand_end_block)] = one;

                        }
                    }
                    catch
                    {

                    }

                }
            }
        }
        public static void clean(Vika_Method method)
        {
        x:
            for (int i = 0; i < method.blocks.Count; i++)
            {
                int value = method.blocks[i].offset;
                for (int z = 0; z < method.blocks.Count; z++)
                {
                    int min = method.blocks[z].offset;
                    int max = method.blocks[z].end;
                    if ((value > min && value <= max)|| (value > min && value < max))
                    {
                        if (method.blocks[z].instrs.Exists(ldd => ldd.offset == method.blocks[i].instrs.First().offset))
                        {
                            method.blocks.RemoveAt(i);
                            goto x;
                        }

                    }
                }
            }
        }
        public static int stage_number() { return 5; }

        public override string who()
        {
            throw new NotImplementedException();
        }
    }
}
