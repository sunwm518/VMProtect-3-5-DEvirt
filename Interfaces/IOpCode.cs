using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.IO;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based
{
   public abstract class IOpCode
    {
        public abstract bool is_opcode(MethodDef md);
        public abstract Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context ctx);
        public abstract bool nop_before();
        public abstract bool is_specially();

        public abstract int Op_Size();



    }
}