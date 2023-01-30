namespace Vika_Anti_VMP_OOP_3._5_Based.Ariphmetics
{
    using dnlib.DotNet;
    using dnlib.DotNet.Emit;
    using System.Collections.Generic;
    using System.IO;
    using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;
    using Vika_Anti_VMP.Helpers;

    /// <summary>
    /// Defines the <see cref="Rem" />.
    /// </summary>
    internal class Xor : IOpCode
    {
        /// <summary>
        /// The is_opcode.
        /// </summary>
        /// <param name="md">The md<see cref="MethodDef"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool is_opcode(MethodDef md)
        {
            List<OpCode> codes_orig = new List<OpCode>();
            md.Body.SimplifyMacros(null);
            foreach (var instr in md.Body.Instructions)
            {
                codes_orig.Add(instr.OpCode);
            }
            if (codes_orig.Count == pattern.Count)
            {
                for (int i = 0; i < codes_orig.Count; i++)
                {
                    if (codes_orig[i] != pattern[i]) return false;
                }
            }
            else
            {
                return false;
            }

            var add_oper = md.Body.Instructions[10].Operand as MethodDef;
            Mutation_Cleaner.clean(add_oper);
            foreach (var instr in add_oper.Body.Instructions)
            {
                if (instr.OpCode == OpCodes.Add)
                {

                    return false;
                }
            }
            foreach (var instr in add_oper.Body.Instructions)
            {

                if (instr.OpCode == OpCodes.Xor)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Defines the pattern.
        /// </summary>
        internal List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
            OpCodes.Call,
            OpCodes.Stloc,
            OpCodes.Ldarg,

            OpCodes.Call,
            OpCodes.Stloc,
            OpCodes.Ldarg,
            OpCodes.Ldarg,

            OpCodes.Ldloc,
            OpCodes.Ldloc,

            OpCodes.Call,
            OpCodes.Call,
            OpCodes.Ret

        };

        /// <summary>
        /// The restore_instruction.
        /// </summary>
        /// <param name="read">The read<see cref="BinaryReader"/>.</param>
        /// <param name="md">The md<see cref="MethodDef"/>.</param>
        /// <returns>The <see cref="Vika_Instruction"/>.</returns>
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            return new Vika_Instruction(OpCodes.Xor);
        }

        /// <summary>
        /// The Op_Size.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int Op_Size()
        {
            return 0;
        }

        /// <summary>
        /// The nop_before.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool nop_before()
        {
            return false;
        }

        /// <summary>
        /// The nop_before.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool is_specially()
        {
            return false;
        }
    }
}
