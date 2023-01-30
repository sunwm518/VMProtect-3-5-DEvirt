using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Interfaces;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Code_Cleanup
{
    class Ldarg_fixer : ICleaner
    {
        public override void Run(Vika_Context vika)
        {
            foreach(var method in vika.devirt_context.disassembled_mds)
            {
                Dictionary<object, Instruction> args = new Dictionary<object, Instruction>();
                var instrs = method.method.Body.Instructions;

               for (int i = 0; i < instrs.Count; i++)
                {
                  if(instrs[i].IsLdarg() && instrs[i+1].IsStloc())
                    {
                        try
                        {
                            args.Add(instrs[i + 1].Operand, instrs[i]);
                        }
                        catch
                        {

                        }

                    }
                }
               foreach(var instr in instrs)
                {
                    if (instr.IsLdloc())
                    {
                        if (args.ContainsKey(instr.Operand))
                        {
                            instr.OpCode = args[instr.Operand].OpCode;
                            instr.Operand = args[instr.Operand].Operand;
                        }
                    }
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
