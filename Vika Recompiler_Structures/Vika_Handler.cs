using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures
{
    public class Vika_Handler
    {
        public Vika_Handler()
        { }

        public short eh_type { get; set; }
        public int try_offset { get; set; }
        public int try_len { get; set; }

        public int unknown_offset_ehs { get; set; }
        public int handler_offset { get; set; }
        public int handler_end { get; set; }


        public int token_catch { get; set; }


    }
}
