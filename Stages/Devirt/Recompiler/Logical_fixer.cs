using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP.Helpers;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Recompiler
{
    class Logical_fixer : IDevirtStage
    {
        public override void Run(Devirt_Context ctx, Vika_Context vika)
        {
            for (int i = 0; i < ctx.disassembled_mds.Count; i++)
            {
                fix_special_opcodes(ctx.disassembled_mds[i],vika);
                fix_fields_opcodes(ctx.disassembled_mds[i], vika);

            }
        }

        private void fix_fields_opcodes(Vika_Method vika_Method, Vika_Context vika)
        {
           var fields = vika_Method.method.Body.Instructions.Where(instr => instr.OpCode == OpCodes.Ldfld).ToList();
            var method = vika_Method.method;
            foreach(var field in fields)
            {
                int index = method.Body.Instructions.IndexOf(field);
                if (index >= 2)
                {
                    if(method.Body.Instructions[index-1].OpCode == OpCodes.Nop && method.Body.Instructions[index - 2].OpCode == OpCodes.Ldnull)
                    {
                        method.Body.Instructions[index].OpCode = OpCodes.Ldsfld;
                        method.Body.Instructions[index - 2].OpCode = OpCodes.Nop;
                    }
                }
            }
        }

        private void fix_special_opcodes(Vika_Method vika_Method, Vika_Context ctx)
        {
            List<Vika_Pattern> patterns = new List<Vika_Pattern>();
            foreach (var field in typeof(Logicals_Patterns_Opcodes).GetFields())
            {
                var patter = (List<OpCode>)field.GetValue(null);
                var code_field = (OpCode)typeof(OpCodes).GetFields().Where(fieldo => fieldo.Name.ToLower() == field.Name.ToLower()).ToList()[0].GetValue(null);
                patterns.Add(new Vika_Pattern() { pattern = patter, opcode = code_field });
            }
            List<int> targets = new List<int>();
            foreach(var instr in vika_Method.method.Body.Instructions)
            {
                if (instr.OpCode == OpCodes.And) targets.Add(vika_Method.method.Body.Instructions.IndexOf(instr));
            }
            foreach (var target in targets)
            {
                foreach (var pattern in patterns)
                {
                    if (is_opcode(pattern, vika_Method.method.Body.Instructions, target))
                    {                     
                        fix(pattern, vika_Method.method.Body.Instructions, target);
                        ctx.logger.debug("Fix Logical: " + pattern.opcode.Name);
                        goto end;
                    }
                }
            end:;
            }
       
        }
        private static bool is_opcode(Vika_Pattern vika, IList<Instruction> instrs, int target)
        {
            if (target > vika.pattern.Count)
            {
                bool sucess = true;
                for (int j = 0; j < vika.pattern.Count; j++)
                {
                    if (instrs[target - j].OpCode != vika.pattern[j]) sucess = false;
                }
                return sucess;
            }
            else return false;

        }

        private static void fix(Vika_Pattern vika, IList<Instruction> instrs, int target)
        {
            for (int j = 0; j < vika.pattern.Count; j++)
            {
                instrs[target - j].OpCode = OpCodes.Nop;
            }
           instrs[target-1].OpCode = vika.opcode;
        }
        public static int stage_number() { return 7; }
        public override string who()
        {
            throw new NotImplementedException();
        }
    }
}
