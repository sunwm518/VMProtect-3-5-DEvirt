using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Stages.Before_Devirt;
using Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based
{
   public class Vika_Context
    {
        public ILogger logger { get; set; }
        public string path { get; set; }
        public ModuleDefMD module { get; set; }
        public Stack<int> value_stack { get;set; }
        public Pre_Devirt_Context pre_context { get; internal set; }
        public Devirt_Context devirt_context { get; internal set; }

        public Stack<Tuple<int,MethodDef>> devirt_methods_stack;

        public Dictionary<int, MethodDef> all_devirts;

    }
}
