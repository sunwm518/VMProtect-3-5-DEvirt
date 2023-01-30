using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures
{
   public class Vika_Method
    {
        internal List<Instruction> parameters;

        public MethodDef method { get; set; }
        public int offset { get; set; }
        public List<Vika_Block> blocks { get; set; }
        public List<Vika_Handler> handlers { get; set; }
        public Stack<Vika_Handler> end_handler;
        public Stack<Vika_Handler> end_handler_second;


    }
}
