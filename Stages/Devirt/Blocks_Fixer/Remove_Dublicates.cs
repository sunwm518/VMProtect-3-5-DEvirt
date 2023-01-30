using de4dot.blocks;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Blocks_Sorter
{
    class Remove_Dublicates : IDevirtStage
    {
        public override void Run(Devirt_Context ctx, Vika_Context vika)
        {
            for (int i = 0; i < ctx.disassembled_mds.Count; i++)
            {
                clean(ctx.disassembled_mds[i]);
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
                    bool du = false;
                if (method.blocks[z].bl_type == Vika_BL_TYPE.Logical_Branch && method.blocks[i].bl_type == Vika_BL_TYPE.Logical_Branch && method.blocks[z].instrs[method.blocks[z].instrs.Count-3].offset == method.blocks[i].instrs[method.blocks[i].instrs.Count - 3].offset && z!=i) { du = true; goto n; }
                //  if (method.blocks[z].instrs.Exists(u => u.offset == method.blocks[i].instrs.First().offset)) { du = true; goto n; }



                n:
                 
                    if (du && method.blocks[i].size > method.blocks[z].size )
                    {
                        //foreach (var pic in method.blocks[z].instrs)
                        //{
                        //    if (method.blocks[i].instrs.Exists(u => u.offset == pic.offset))
                        ////    {
                        //        if (!method.blocks[i].instrs[method.blocks[i].instrs.Count-2])
                        //        {
                        //            method.blocks[z].secure = true;
                        //            goto x;
                        //        }
                        //        else
                        //        {
                                    bool fixec = false;
                                    method.blocks.RemoveAt(z);
                                    goto x;
                               // }
                                
                        //    }
                        //}
                    }
                    else if (du && method.blocks[i].size < method.blocks[z].size )
                    {
                        //foreach (var pic in method.blocks[i].instrs)
                        //{
                        //    if (!method.blocks[z].instrs.Exists(u => u.offset == pic.offset))
                        //    {
                        //        method.blocks[i].secure = true;
                        //        goto x;
                        //    }
                        //    else
                        //    {
                        //        if (method.blocks[z].instrs.Exists(u => u.offset == pic.offset))
                        //        {

                                    method.blocks.RemoveAt(i);
                                    goto x;
                        //        }
                        //    }
                        //}
                    }
                    else if (du && method.blocks[i].size == method.blocks[z].size )
                    {

                        method.blocks.RemoveAt(i);
                        goto x;
                    }

                m:;
                }
            }



        g:



            for (int i = 0; i < method.blocks.Count; i++)
            {
                var b = method.blocks[i];
                for (int h = 0; h < method.blocks.Count; h++)
                {
                    var z = method.blocks[h];
                    if (   z.bl_type == Vika_BL_TYPE.Logical_Branch && b.bl_type == Vika_BL_TYPE.Logical_Branch && method.blocks[h].instrs[method.blocks[h].instrs.Count - 3].offset == method.blocks[i].instrs[method.blocks[i].instrs.Count - 3].offset && z != b )
                    {
                     


                        Console.WriteLine(b.target);
                        Console.WriteLine(z.target);

                        Console.ReadLine();
                    }
                }
            }

            //foreach(var block in method.blocks)
            //    {
            //        foreach(var instr in block.instrs)
            //        {
            //            foreach (var block2 in method.blocks)
            //            {
            //                if (block2 == block) continue;
            //                foreach (var instr2 in block2.instrs)
            //                {
            //                    if (instr.offset == instr2.offset) {
            //                        Console.WriteLine(instr.offset);
            //                        Console.WriteLine("Z");
            //                        Console.WriteLine(block.offset);
            //                        Console.WriteLine(block.bl_type);
            //                        Console.WriteLine(block.target);
            //                        Console.WriteLine(block.size);
            //                        foreach (var instr3 in block.instrs)
            //                        {
            //                            Console.WriteLine(instr3.Instruction.OpCode.Name);

            //                            Console.WriteLine(instr3.offset);

            //                        }
            //                        Console.WriteLine("Z");
            //                        Console.WriteLine(block2.offset);
            //                        Console.WriteLine(block2.bl_type);
            //                        Console.WriteLine(block2.target);
            //                        Console.WriteLine(block2.size);

            //                        foreach (var instr4 in block2.instrs)
            //                        {
            //                            Console.WriteLine(instr4.Instruction.OpCode.Name);

            //                            Console.WriteLine(instr4.offset);

            //                        }
            //                       // Console.ReadLine();
            //                    }


            //                }
            //            }
            //        }
            //    }



            //        List<Vika_Block> blocks = new List<Vika_Block>();
            //    Dictionary<Vika_Block, Vika_Block> logicals = new Dictionary<Vika_Block, Vika_Block>();
            //pp:
            //    for (int i = 0; i < method.blocks.Count; i++)
            //    {

            //        if (method.blocks[i].bl_type == Vika_BL_TYPE.Logical_Branch)
            //        {
            //            int target = method.blocks[i].target;
            //            blocks.Add(method.blocks[i]);
            //            var m = method.blocks.Where(bl => bl.instrs.First().offset == target);
            //            if (m.Count() == 0)
            //            {
            //                var nn = method.blocks.Where(bl => bl.offset == target);
            //                if (nn.Count() == 0)
            //                {
            //                    Console.WriteLine("None");
            //                    Console.ReadLine();
            //                }

            //            }
            //            else
            //            {
            //              method.blocks.RemoveAt(i);

            //              //  blocks.Add(m.ToList()[0]);
            //                  // method.blocks.RemoveAt(method.blocks.IndexOf(m.ToList()[0]));
            //              //  goto pp;
            //            }
            //        }
            //        else
            //        {
            //      //     blocks.Add
            //        }


            //    }
            ////
            //    method.blocks = blocks;

            //    Console.WriteLine(blocks.Count);


















            {
                int hamma = 0;
            z:
                for (int i = 0; i < method.blocks.Count; i++)
                {
                 
                    var block = method.blocks[i];

                    //if (block.bl_type == Vika_BL_TYPE.Logical_Branch)
                    //{
                    //    if ((method.blocks.Count > i + 1))
                    //    {
                    //        if (block.target != method.blocks[i + 1].instrs.First().offset)
                    //        {
                    //            Console.WriteLine("================= " + block.instrs.Last().Instruction.OpCode.Name + " ================ZZZZZZZZZZZZZZZZ=================");
                    //            Console.WriteLine(block.target);
                             
                    //                var two = method.blocks.Where(bl => bl.offset == block.target);
                    //                if (two.Count() == 1)
                    //                {

                    //                    var tt = two.ToList()[0];

                    //                    if (method.blocks.IndexOf(tt) - 1 >= 0)
                    //                    {
                    //                    var aa =method.blocks[ method.blocks.IndexOf(method.blocks[i + 1])];
                    //                    var aaa = method.blocks.IndexOf(method.blocks[i + 1]);


                    //                    method.blocks[aaa] = block;
                    //                    method.blocks[i] = aa;


                    //               //     method.blocks.Insert(method.blocks.IndexOf(tt), block);

                    //                    }
                    //                    else
                    //                    {
                    //                    var aa = method.blocks[method.blocks.IndexOf(method.blocks[i + 1])];
                    //                    var aaa = method.blocks.IndexOf(method.blocks[i + 1]);


                    //                    method.blocks[aaa] = block;
                    //                    method.blocks[i] = aa;
                    //                    }

                    //                }
                    //                else if (two.Count() > 1)
                    //                {
                    //                    Console.WriteLine(" Found");
                    //                    Console.ReadLine();
                    //                }
                    //                else
                    //                {
                    //                    Console.WriteLine("Not Found");
                    //                    Console.ReadLine();


                    //                }

                                
                    //        }
                    //    }

                    //    else
                    //    {
                    //        var two = method.blocks.Where(bl => bl.offset == block.target);

                    //        Console.WriteLine("================= " + block.instrs.Last().Instruction.OpCode.Name + " =================================");
                    //        Console.WriteLine(block.target);
                    //        if (two.Count() == 1)
                    //        {
                    //            var tt = two.ToList()[0];
                    //            method.blocks.Remove(block);

                    //            method.blocks.Insert(method.blocks.IndexOf(tt), block);

                    //            goto z;
                    //        }
                    //        else if (two.Count() > 1)
                    //        {
                    //            Console.WriteLine(" Found");
                    //            Console.ReadLine();
                    //        }
                    //        else
                    //        {

                    //            var three = method.blocks.Where(bl => bl.instrs.Exists(o => o.offset == block.target));
                    //            if (three.Count() == 1)
                    //            {
                    //                var tt = two.ToList()[0];

                    //                method.blocks.Remove(block);

                    //                method.blocks.Insert(method.blocks.IndexOf(tt), block);

                    //                goto z;


                    //            }
                    //            else if (three.Count() > 1)
                    //            {
                    //                Console.WriteLine(" Found");
                    //                Console.ReadLine();
                    //            }
                    //            else
                    //            {

                    //                Console.WriteLine("Not Found");
                    //                Console.ReadLine();
                    //            }

                    //        }

                    //    }
                    //}
                }

            }


        }
        public static int stage_number() { return 2; }

        public override string who()
        {
            throw new NotImplementedException();
        }
    }
}
