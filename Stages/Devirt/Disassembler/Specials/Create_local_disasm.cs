using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Disassembler.Specials
{
    class Create_local_disasm
    {
        public static void disasm(Vika_Block block, IOpCode code, List<Vika_Instruction> instrs, Vika_Instruction instr, Vika_Context ctx, Vika_Method md)
        {
            int c = instrs.Count;
            if (instrs[c - 1].Instruction.IsLdcI4() && instrs[c- 2].code == OpCodes.Dup && instrs[c - 3].code == OpCodes.Ldnull)
            {
                instrs.RemoveAt(c - 1);
                instrs.RemoveAt(c - 2);
                instrs.RemoveAt(c - 3);
             //   instrs.Add(instr);
              //  instrs.Last().nop();
              //  instrs.RemoveAt(c - 4);
            
            }
            else if (instrs[c - 1].Instruction.IsLdcI4() && instrs[c - 2].code == OpCodes.Ldnull && instrs[c - 3].code == OpCodes.Ldelem_Ref && instrs[c-4].Instruction.IsLdcI4())
            {
                int number = instrs[c - 4].Instruction.GetLdcI4Value();

                if (number == 0 && md.parameters.Count == 0)
                {
          

                    instrs.RemoveAt(c - 1);
                    instrs.RemoveAt(c - 2);
                    instrs.RemoveAt(c - 3);
                    instrs.RemoveAt(c - 4);

                    //instrs.Add(instr);
                    //instrs.Last().restore();
                }
                else
                {
                    instrs[c - 1].Instruction = md.parameters[number];
           
                  //  instrs.RemoveAt(c - 1);
                    instrs.RemoveAt(c - 2);
                    instrs.RemoveAt(c - 3);
                    instrs.RemoveAt(c - 4);
                    instrs.RemoveAt(c - 5);

                    instrs.Add(instr);
                    instrs.Last().restore();
                }

                
               
            }
            else if (instrs[c - 1].Instruction.OpCode == OpCodes.Nop && instrs[c - 2].code == OpCodes.Ldnull && instrs[c - 3].code == OpCodes.Ldelema && instrs[c - 4].Instruction.IsLdcI4())
            {
                int number = instrs[c - 4].Instruction.GetLdcI4Value();
                if (number == 0 && md.parameters.Count == 0)
                {
                    instrs.RemoveAt(c - 1);

                    instrs.RemoveAt(c - 2);
                    instrs.RemoveAt(c - 3);
                    instrs.RemoveAt(c - 4);
                    //instrs.Add(instr);
                    //instrs.Last().restore();
                }
                else
                {
                    instrs[c - 1].Instruction = md.parameters[number];
                    instrs.RemoveAt(c - 2);
                    instrs.RemoveAt(c - 3);
                    instrs.RemoveAt(c - 4);
                    instrs.RemoveAt(c - 5);
                 

                    instrs.Add(instr);
                    instrs.Last().restore();
                }
            }
            else if (instrs[c - 1].Instruction.IsLdcI4() && instrs[c - 2].code == OpCodes.Dup && instrs[c - 3].code == OpCodes.Ldelem_Ref && instrs[c - 4].Instruction.IsLdcI4())
            {
                int number = instrs[c - 4].Instruction.GetLdcI4Value();
                if (number == 0 && md.parameters.Count == 0)
                {
                    instrs.RemoveAt(c - 1);
           
                    instrs.RemoveAt(c - 2);
                    instrs.RemoveAt(c - 3);
                    instrs.RemoveAt(c - 4);
                    //instrs.Add(instr);
                    //instrs.Last().restore();
                }
                else
                {
                    instrs[c - 1].Instruction = md.parameters[number];
                    instrs.RemoveAt(c - 2);
                    instrs.RemoveAt(c - 3);
                    instrs.RemoveAt(c - 4);
                    instrs.RemoveAt(c - 5);

                    instrs.Add(instr);
                    instrs.Last().restore();
                }
            }
            else
            {
                throw new Exception();
            }
            block.bl_type = Vika_BL_TYPE.Normal;

        }
    }
}
