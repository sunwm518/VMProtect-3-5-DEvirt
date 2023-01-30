//using dnlib.DotNet.Emit;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

//namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Blocks_Fixer
//{
//    class Targets_Fixer : IDevirtStage
//    {
//        public override void Run(Devirt_Context ctx, Vika_Context vika)
//        {
//            for (int i = 0; i < ctx.disassembled_mds.Count; i++)
//            {
//                fix_branches(ctx.disassembled_mds[i], ctx.disassembled_mds);
//            }
//        }
//        public static void fix_branches(Vika_Method method, List<Vika_Method> alls)
//        {
//            try
//            {
//                var md = method.method;
//                var blocks = method.blocks;
//            z:
//                List<Vika_Block> not_blocks = new List<Vika_Block>();

//                foreach (var bl in method.blocks)
//                {
//                    if (bl.bl_type == Vika_BL_TYPE.Logical_Branch && !bl.fixed_)
//                    {
//                        not_blocks.Add(bl);
//                    }
//                }
//                for (int i = 0; i < not_blocks.Count; i++)
//                {
//                    if (blocks.IndexOf(not_blocks[i]) < blocks.Count - 1)
//                    {
//                        if (not_blocks[i].target != blocks[blocks.IndexOf(not_blocks[i]) + 1].offset)
//                        {
//                            if (method.blocks.Exists(sd => sd.offset == not_blocks[i].target))
//                            {
//                                var one = method.blocks.Single(sd => sd.offset == not_blocks[i].target);
//                                var two = method.blocks[method.blocks.IndexOf(blocks[blocks.IndexOf(not_blocks[i]) + 1])];
//                                method.blocks[method.blocks.IndexOf(one)] = two;
//                                method.blocks[method.blocks.IndexOf(two)] = one;

//                            }
//                            else
//                            {

//                                var one = method.blocks.Single(sd => sd.instrs.Exists(intr => intr.offset == not_blocks[i].target));
//                                var two = method.blocks[method.blocks.IndexOf(blocks[blocks.IndexOf(not_blocks[i]) + 1])];
//                                method.blocks[method.blocks.IndexOf(one)] = two;
//                                method.blocks[method.blocks.IndexOf(two)] = one;
//                            }

//                        }
//                    }
//                }
//            }
//            catch
//            {

//            }
//        }
//        public override string who()
//        {
//            throw new NotImplementedException();
//        }
//        public static int stage_number() { return 4; }

//    }
//}
