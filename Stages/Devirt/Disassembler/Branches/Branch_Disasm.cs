using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using Vika_Anti_VMP.Helpers;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Disassembler.Branches
{
    internal class Branch_Disasm
    {
        public static void disasm(Vika_Block block, IOpCode code, List<Vika_Instruction> instrs, Vika_Instruction instr, Devirt_Context ctx)
        {
            List<Vika_Pattern> patterns = new List<Vika_Pattern>();
            foreach (System.Reflection.FieldInfo field in typeof(Logicals_Patterns_Branches).GetFields())
            {
                List<OpCode> patter = (List<OpCode>)field.GetValue(null);
                OpCode code_field = (OpCode)typeof(OpCodes).GetFields().Where(fieldo => fieldo.Name.ToLower() == field.Name.ToLower()).ToList()[0].GetValue(null);
                patterns.Add(new Vika_Pattern() { pattern = patter, opcode = code_field });
            }
            patterns.Add(new Vika_Pattern() { pattern = Logicals_Patterns_Special.mem_prot, opcode = OpCodes.Br });
            patterns.Add(new Vika_Pattern() { pattern = Logicals_Patterns_Special.read_addr, opcode = OpCodes.Brtrue });
            bool fixed_ = false;
            foreach (Vika_Pattern pattern in patterns)
            {

                if (is_branch(pattern, instrs))
                {

                    if (pattern.pattern == Logicals_Patterns_Special.mem_prot)
                    {
                        fix_memory_protect(pattern, ctx, instrs, block, instr);
                    }
                    List<int> offsets = new List<int>();
                    if (pattern.opcode == OpCodes.Br)
                    {
                        offsets.Add(ctx.vika.value_stack.Pop());
                    }
                    else
                    {
                        offsets.Add(ctx.vika.value_stack.Pop());
                        offsets.Add(ctx.vika.value_stack.Pop());
                    }
                    fix(pattern, offsets, ctx, instrs, block, instr);
                    ctx.vika.logger.debug("Disassembled Branch: " + pattern.opcode.Name);
                    goto end;
                }
            }
            if (!fixed_) throw new Exception("JMP not fixed"); 
        end:;
        }

        private static void fix_memory_protect(Vika_Pattern pattern, Devirt_Context ctx, List<Vika_Instruction> instrs, Vika_Block block, Vika_Instruction instr)
        {
            bool flag = false;
            int count = 0;
            int c = instrs.Count;
            while (!flag)
            {
                if (instrs[c - 1 - count].Instruction.OpCode == OpCodes.Ldc_I4)
                {
                    ctx.vika.value_stack.Push(instrs[c - 1 - count].Instruction.GetLdcI4Value());
                }

                if (instrs[c - 1 - count].Instruction.OpCode == OpCodes.Ldnull)
                {
                    flag = true;
                }

                if (instrs[c - 1 - count].Instruction.OpCode == OpCodes.Box)
                {
                    flag = true;
                }
                else
                {
                    instrs[c - 1 - count].code = OpCodes.Nop;
                    instrs[c - 1 - count].Instruction = Instruction.Create(OpCodes.Nop);
                }
                count++;
            }
        }

        private static bool is_branch(Vika_Pattern vika, List<Vika_Instruction> instrs)
        {
            if (instrs.Count >= vika.pattern.Count)
            {
                bool sucess = true;
                int c = instrs.Count;
                for (int j = 0; j < vika.pattern.Count; j++)
                {


                    if (instrs[c - j - 1].code != vika.pattern[j])
                    {
                        sucess = false;
                    }


                }
                return sucess;
            }
            else
            {
                return false;
            }
        }
        private static void fix(Vika_Pattern vika, List<int> offsets, Devirt_Context ctx, List<Vika_Instruction> instrs, Vika_Block block, Vika_Instruction instr)
        {
            int c = instrs.Count;
            for (int j = 0; j < vika.pattern.Count; j++)
            {
                instrs[c - 1 - j].nop();
            }
           
            if (offsets[0] == 0)
            {
                instr.code = OpCodes.Ret;
                instr.offset = 0;
                instrs.Add(instr);
                c = instrs.Count;
                instrs[c - 1].Instruction = Instruction.Create(OpCodes.Ret);
                instrs[c - 1].target = offsets[0];
                block.bl_type = Vika_BL_TYPE.Ret;
                return;
            }
            if (offsets.Count == 1)
            {
                instr.code = vika.opcode;
                instr.target = offsets[0];
                if (!ctx.offsets.offsets.Contains((offsets[0])))
                {
                    ctx.offsets.push_end(offsets[0]);
                }
                instrs.Add(instr); 
                c = instrs.Count;
                instrs[c - 1].Instruction = Instruction.Create(vika.opcode, new Instruction());
                block.bl_type = Vika_BL_TYPE.Br;
            }
            else
            {
                instr.code = vika.opcode;
                instr.target = offsets[1];
                if (!ctx.offsets.offsets.Contains((offsets[1])))
                {
                    ctx.offsets.push_end(offsets[1]);
                }
                instrs.Add(instr);
                c = instrs.Count;
                instrs[c - 1].Instruction = Instruction.Create(vika.opcode, new Instruction());

                instrs.Add(new Vika_Instruction(OpCodes.Nop));//very important
                c = instrs.Count;

                instrs[c - 1].Instruction = Instruction.Create(OpCodes.Nop);
                instrs[c - 1].offset = int.MaxValue;

                instrs.Add(new Vika_Instruction(OpCodes.Br));
                c = instrs.Count;

                instrs[c - 1].Instruction = Instruction.Create(OpCodes.Br, new Instruction());
                instrs[c - 1].target = offsets[0];
                instrs[c - 1].offset = 110921410;

             

                block.bl_type = Vika_BL_TYPE.Logical_Branch;
                block.target = offsets[0];
                block.fixed_ = false;
                if (!ctx.offsets.offsets.Contains((offsets[0])))
                {
                    ctx.offsets.push(offsets[0]);
                }
            }

        }
    }
}
