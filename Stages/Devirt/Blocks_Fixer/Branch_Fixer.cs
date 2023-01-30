using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Blocks_Fixer
{
    class Branch_Fixer : IDevirtStage
    {
        public override void Run(Devirt_Context ctx, Vika_Context vika)
        {
            for (int i = 0; i < ctx.disassembled_mds.Count; i++)
            {
                fix_branches(ctx.disassembled_mds[i], ctx.disassembled_mds);
            }
        }
        public static void fix_branches(Vika_Method method, List<Vika_Method> alls)
        {
            var md = method.method;
            foreach (var bl in method.blocks)
            {
                foreach (var instr in bl.instrs)
                {
                    if (instr.code == OpCodes.Br || instr.code == OpCodes.Ble || instr.code == OpCodes.Brfalse || instr.code == OpCodes.Brtrue || instr.code == OpCodes.Blt_Un || instr.code == OpCodes.Beq || instr.code == OpCodes.Bge || instr.code == OpCodes.Bge_Un
                      || instr.code == OpCodes.Bgt || instr.code == OpCodes.Bgt_Un || instr.code == OpCodes.Ble || instr.code == OpCodes.Ble_Un || instr.code == OpCodes.Blt || instr.code == OpCodes.Blt_Un || instr.code == OpCodes.Bne_Un || instr.code == OpCodes.Leave )
                    {
                        try
                        {
                            int offset_to_br = instr.target;
                            Vika_Instruction instruction_for_branch = null;
                            if (method.blocks.Where(sd => sd.instrs.Exists(ins => ins.offset == offset_to_br)).ToList().Count == 0)
                        {
                                if (method.blocks.Where(sd => sd.offset == offset_to_br).ToList().Count>0)
                                {
                                    Console.WriteLine("Found");
                                }
                                else
                                {
                                    Console.WriteLine(instr.code.Name);
                                    Console.WriteLine(instr.target);
                                    Console.WriteLine(method.method.Name);
                                    Console.WriteLine("Not Found");
                                }
                        }
                            else
                            {
                                var br_block = method.blocks.Where(sd => sd.instrs.Exists(ins => ins.offset == offset_to_br)).ToList()[0];
                                instruction_for_branch = br_block.instrs.Single(ins => ins.offset == offset_to_br);
                                method.blocks[method.blocks.IndexOf(bl)].instrs[method.blocks[method.blocks.IndexOf(bl)].instrs.IndexOf(instr)].Instruction.Operand = instruction_for_branch.Instruction;
                            }
                      
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("==============" + ex.ToString());
                            Console.WriteLine(instr.code.Name);
                            Console.WriteLine(instr.target);
                            Console.WriteLine(method.method.Name);


                        }
                    }
                }
            }
        }
        public override string who()
        {
            throw new NotImplementedException();
        }
        public static int stage_number() { return 3; }

    }
}
