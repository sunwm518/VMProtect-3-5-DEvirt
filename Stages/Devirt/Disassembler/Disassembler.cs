using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Helpers;
using Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Disassembler.Branches;
using Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Disassembler.Specials;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Disassembler
{
    class Disassembler : IDevirtStage
    {
        public override void Run(Devirt_Context ctx, Vika_Context vika)
        {
            var methods = vika.pre_context.virt_methods;
            vika.devirt_context = ctx;
            List<Vika_Method> disassembled_methods = new List<Vika_Method>();
            vika.value_stack = new Stack<int>();
            vika.devirt_methods_stack = new Stack<Tuple<int, dnlib.DotNet.MethodDef>>();
            vika.all_devirts = methods;
            foreach (var md in methods)
            {
                vika.devirt_methods_stack.Push(new Tuple<int, dnlib.DotNet.MethodDef>(md.Key, md.Value));
            }
            while (vika.devirt_methods_stack.Count > 0)
            {
                var method = vika.devirt_methods_stack.Pop();
                method.Item2.Body.OptimizeMacros();
                Vika_Method disassembled = new Vika_Method();
                disassembled.end_handler = new Stack<Vika_Handler>();
                disassembled.end_handler_second = new Stack<Vika_Handler>();

                var params_m = method.Item2.Body.Instructions.Where(instr => instr.IsLdarg()).ToList();
                if (params_m.Count == 0) params_m = method.Item2.Parameters.ToList().Select(s => Instruction.Create(OpCodes.Ldarg, s)).ToList();
                method.Item2.Body.Variables.Clear();
                disassembled.parameters = params_m;
                ctx.offsets = new VikaStack();
                disassembled.blocks = new List<Vika_Block>();
                ctx.offsets_list = new List<int>();
                disassembled.method = method.Item2;
                disassembled.offset = method.Item1;
                disassembled.handlers = new List<Vika_Handler>();
                bool one_block = true;
                VikaReader reader = new VikaReader(method.Item1, vika.module);
                ctx.offsets.push(method.Item1);
                vika.logger.debug("Dissasemble Start method: " + method.Item2.FullName);
                while (ctx.offsets.offsets.Count > 0)
                {

                    int current_offset = ctx.offsets.pop();
                    int offset_for_instrs = current_offset;
                    if (!ctx.offsets_list.Contains(current_offset))
                    {
                        try
                        {
                            ctx.offsets_list.Add(current_offset);
                            reader.set_offset(current_offset);
                            vika.logger.debug("Dissasemble block at offset: " + current_offset);
                            bool end_block = false;
                            Vika_Block block = new Vika_Block();
                            block.md = method.Item2;
                            var instrs_restore = new List<Vika_Instruction>();
                            while (!end_block)
                            {
                                try
                                {
                                    byte opcode = reader.ReadByte();
                                    if (!vika.pre_context.structured_all_opcodes.ContainsKey(opcode)) throw new Exception(opcode.ToString());
                                    var vika_code = vika.pre_context.structured_all_opcodes[opcode].vika_opcode;
                                    block.is_first = one_block;
                                    one_block = false;
                                    var vika_instr = vika_code.restore_instruction(reader.reader, disassembled, vika);
                                    vika_instr.offset = offset_for_instrs;
                                    offset_for_instrs += vika_code.Op_Size();
                                    vika.logger.debug("Dissasemble instruction " + vika_code.ToString() + " at offset: " + vika_instr.offset + " at method: " + method.Item2.Name + " OpCode: " + vika_instr.code.Name);
                                    if (!vika_code.is_specially() && vika_code.nop_before())
                                    {
                                        disassemble_special_block(block, vika_code, instrs_restore, vika_instr, ctx, disassembled);
                                    }
                                    else if (!vika_code.is_specially() && !vika_code.nop_before())
                                    {
                                        disassemble_normal_block(block, vika_code, instrs_restore, vika_instr);
                                    }
                                    else if (vika_code.is_specially() && !vika_code.nop_before())
                                    {
                                        disassemble_leave_block(disassembled, vika, instrs_restore, vika_instr, block);
                                    }
                                    else if (vika_code.is_specially() && vika_code.nop_before())
                                    {
                                        Branch_Disasm.disasm(block, vika_code, instrs_restore, vika_instr, ctx);
                                        end_block = true;
                                    }
                                    offset_for_instrs++;
                                    if (end_block)
                                    {
                                        block.instrs = instrs_restore;
                                        block.offset = current_offset;
                                        block.end = offset_for_instrs;
                                        block.size = block.instrs.Count;
                                        block.fixed_ = block.bl_type != Vika_BL_TYPE.Logical_Branch;
                                        disassembled.blocks.Add(block);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ctx.vika.logger.info("ERROR::::" + ex.ToString());
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ctx.vika.logger.info("ERROR::::" + ex.ToString());
                        }
                    }



                }
                vika.logger.debug("Dissasemble End method: " + method.Item2.FullName);
                disassembled_methods.Add(disassembled);
            }
            ctx.disassembled_mds = disassembled_methods;
        }

        private void disassemble_leave_block(Vika_Method md, Vika_Context vika, List<Vika_Instruction> instrs_restore, Vika_Instruction vika_instr, Vika_Block block)
        {
            if (vika_instr.code == OpCodes.Leave)
                Handlers_Disasm.disasm_leave(md, vika, instrs_restore, vika_instr);
            else Handlers_Disasm.disasm_try(md, vika, instrs_restore, vika_instr);

            instrs_restore.Add(vika_instr);
            instrs_restore.Last().restore();
            block.bl_type = Vika_BL_TYPE.Normal;
        }

        private void disassemble_normal_block(Vika_Block block, IOpCode vika_code, List<Vika_Instruction> instrs_restore, Vika_Instruction instr)
        {
            instrs_restore.Add(instr);
            instrs_restore.Last().restore();
            block.bl_type = Vika_BL_TYPE.Normal;
        }

        private void disassemble_special_block(Vika_Block block, IOpCode code, List<Vika_Instruction> instrs, Vika_Instruction instr, Devirt_Context ctx, Vika_Method md)
        {
            if (code.GetType().FullName.Contains("Local"))
            {
                Create_local_disasm.disasm(block, code, instrs, instr, ctx.vika, md);
            }
            else
            {
                if (instrs.Count > 0)
                    instrs.Last().nop();
                instrs.Add(instr);
                instrs.Last().restore();
                block.bl_type = Vika_BL_TYPE.Normal;
            }
        }

        public static int stage_number() { return 1; }

        public override string who()
        {
            throw new NotImplementedException();
        }
    }
}