using de4dot.blocks;
using de4dot.blocks.cflow;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Collections.Generic;
using System.Linq;

namespace DeMutation
{
    public class ControlFlowDeobfuscator : IBlocksDeobfuscator
    {
        public static Code[] ArithmeticOpCodes;

        private static InstructionEmulator Emulator;

        private Local Context;

        private MethodDef Method;

        private List<Block> VisitedBlocks;

        private static readonly Code[] ArithmethicCodes;

        public bool ExecuteIfNotModified => false;

        public void DeobfuscateBegin(Blocks blocks)
        {
            MethodDef method = (Method = blocks.Method);
            Emulator.Initialize(method);
            Context = FindContext(method);
            VisitedBlocks = new List<Block>();
        }

        public bool Deobfuscate(List<Block> allBlocks)
        {
            if (Context == null)
            {
                return false; // просто нет мутаций
            }
            foreach (Block block in allBlocks)
            {
                ProcessBlock(block);
            }
            return false;
        }

        private void ProcessBlock(Block block, Value value = null)
        {
            if (VisitedBlocks.Contains(block))
                return;
            //уже почистили
            VisitedBlocks.Add(block);
            if (value != null)
                Emulator.SetLocal(Context, value); //ставим рабочую локаль для эмулятора

            //прогоняем клинером инструкции
            foreach (Instr instr in block.Instructions)
            {
                ProcessInstruction(instr.Instruction);
            }

            foreach (Block target in block.GetTargets())
            {
                ProcessBlock(target, Emulator.GetLocal(Context));
            }
        }

        private void ProcessInstruction(Instruction instruction)
        {
            if (instruction.IsStloc())//локале назначается значение скорее всего
            {
                Local local = instruction.GetLocal(Method.Body.Variables);
                if (local == Context)//да, так точно
                {
                    Emulator.Emulate(instruction);
                    return;
                }
                Emulator.Pop(); //видимо выкидываем локаль из стека, ведь она не вмпшная
                Emulator.MakeLocalUnknown(local);//локаль нормальная
            }
            else if (instruction.IsLdloc())//сама логика вмпшки
            {
                Emulator.Emulate(instruction);
                Local local2 = instruction.GetLocal(Method.Body.Variables);
                if (local2 == Context)
                {
                    instruction.OpCode = OpCodes.Ldc_I4;
                    instruction.Operand = (Emulator.Peek() as Int32Value).Value;
                }
            }
            else
            {
                Emulator.Emulate(instruction);
            }
        }

        private Local FindContext(MethodDef method)
        {
            Dictionary<Local, int> LocalFlows = new Dictionary<Local, int>(); //локали и ключи вмп
            LocalList locals = method.Body.Variables; //все локали метода
            foreach (Local local in locals)
            {
                if (local.Type.ElementType == ElementType.U4)
                {
                    LocalFlows.Add(local, 0); //uint4 локаль для мутаций
                }
            }
            IList<Instruction> instructions = method.Body.Instructions;
            for (int i = 0; instructions.Count > i; i++)
            {





                Instruction instr = instructions[i];
                if (instr.OpCode == OpCodes.Ldloca || instr.OpCode == OpCodes.Ldloca_S) //использование мутаций вмп
                {
                    Local local2 = instr.GetLocal(locals);
                    if (LocalFlows.ContainsKey(local2))
                    {
                        LocalFlows.Remove(local2); //удаляем локаль из вмп мутаций
                    }
                }
                else
                {
                    if (!instr.IsStloc()) //назначение ключа
                    {
                        continue;
                    }
                    Local local3 = instr.GetLocal(locals);
                    if (LocalFlows.ContainsKey(local3))//ключ вмп
                    {
                        Instruction before = instructions[i - 1];
                        if (!before.IsLdcI4() && !IsArithmethic(before))
                        {
                            LocalFlows.Remove(local3);
                        }
                        else
                        {
                            LocalFlows[local3]++;
                        }
                    }
                }
            }
            if (LocalFlows.Count == 1)
            {
                return LocalFlows.Keys.ToArray()[0]; //1 локаль, все ок
            }
            if (LocalFlows.Count == 0)
            {
                return null; // нет мутаций
            }
            int highestCount = 0;
            Local highestLocal = null;
            foreach (KeyValuePair<Local, int> entry in LocalFlows)
            {
                if (entry.Value > highestCount)
                {
                    highestLocal = entry.Key;
                    highestCount = entry.Value;
                }
                //для главной локали, большее значение
            }
            return highestLocal;
        }

        private bool IsArithmethic(Instruction instruction)
        {
            return ArithmethicCodes.Contains(instruction.OpCode.Code);
        }

        static ControlFlowDeobfuscator()
        {
            ArithmeticOpCodes = new Code[17]
            {
            Code.Add,
            Code.Shr,
            Code.Mul,
            Code.Div,
            Code.Rem,
            Code.Or,
            Code.And,
            Code.Not,
            Code.Shl,
            Code.Xor,
            Code.Shr_Un,
            Code.Add_Ovf_Un,
            Code.Div_Un,
            Code.Mul_Ovf_Un,
            Code.Sub_Ovf_Un,
            Code.Sub,
            Code.Rem_Un
            };
            Emulator = new InstructionEmulator();
            ArithmethicCodes = ArithmeticOpCodes;
        }
    }
}
