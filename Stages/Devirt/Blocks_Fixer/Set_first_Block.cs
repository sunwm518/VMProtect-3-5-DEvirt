//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

//namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Blocks_Fixer
//{
//    class Set_first_Block : IDevirtStage
//    {
//        public override void Run(Devirt_Context ctx, Vika_Context vika)
//        {
//            for(int i = 0; i < ctx.disassembled_mds.Count; i++)
//            {
//                set_priority(ctx.disassembled_mds[i]);
//            }
//        }
//        public static void set_priority(Vika_Method method)
//        {
            
//            if (!method.blocks[0].is_first)
//            {
//                for (int i = 0; i < method.blocks.Count; i++)
//                {
//                    if (method.blocks[i].is_first)
//                    {
//                        var bl = method.blocks[0];
//                        method.blocks[0] = method.blocks[i];
//                        method.blocks[i] = bl;
//                        return;
//                    }
//                }
//            }
//        }
//        public override string who()
//        {
//            throw new NotImplementedException();
//        }
//        public static int stage_number() { return 3; }

//    }
//}
