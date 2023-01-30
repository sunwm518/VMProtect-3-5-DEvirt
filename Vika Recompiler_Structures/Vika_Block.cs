using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures
{
    public class Vika_Block
    {
        internal Vika_Block last { get; set; }

        public bool fixed_ { get; set; }

        public Vika_Block()
        { }
        public Vika_Block(List<Vika_Instruction> instrs_, int offset_, int br)
        {
            this.instrs = instrs_;
            this.offset = offset_;
        }
        public Vika_BL_TYPE bl_type { get; set; }
        public List<Vika_Instruction> instrs { get; set; }
        public int offset { get; set; }
        public int end { get; set; }
        public int size { get; set; }
        public int target { get; set; }
        public bool is_first { get; set; }
        public MethodDef md { get; set; }
        public bool secure { get; internal set; }
    }
}
