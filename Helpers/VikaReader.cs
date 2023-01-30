using dnlib.DotNet;
using dnlib.PE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vika_Anti_VMP_OOP_3._5_Based.Helpers
{
    class VikaReader
    {
        public VikaReader(int offset, ModuleDefMD mod)
        {
           reader = new BinaryReader(new MemoryStream(mod.Metadata.PEImage.CreateReader(mod.Metadata.PEImage.ToFileOffset((RVA)offset)).ToArray()));
            module = mod;
        }
        public BinaryReader reader { get; set; }
        private ModuleDefMD module { get; set; }
        public void set_offset(int offset)
        {
            reader = new BinaryReader(new MemoryStream(module.Metadata.PEImage.CreateReader(module.Metadata.PEImage.ToFileOffset((RVA)offset)).ToArray()));
        }
        public int ReadInt32()
        {
            return reader.ReadInt32() ;
        }
        public byte ReadByte()
        {
            return reader.ReadByte();
        }
    }
}
