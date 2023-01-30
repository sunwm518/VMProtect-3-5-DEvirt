using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.PE;
using System;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using System.Collections.Generic;
using System.IO;

namespace Vika_Anti_VMP_OOP_3._5_Based.VMP
{
    class External_Call : IOpCode
    {
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

            if (md.Body.Instructions[4].GetLdcI4Value() == 0)
            {
                return true;
            }
            return false;
        }

        public override int Op_Size()
        {
            return x;
        }
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {


            int offset = Convert.ToInt32(vika.value_stack.Pop());





            int one = offset;
            var reader = new BinaryReader(new MemoryStream(vika.module.Metadata.PEImage.CreateReader(vika.module.Metadata.PEImage.ToFileOffset((RVA)offset)).ToArray()));
            int z = 0;
            var params_count = (ushort)reader.ReadInt16();
            z += 2; ;
           var params_t = new TypeSig[params_count];
            if (params_count > 0)
            {
                for (int i = params_count - 1; i >= 0; i--)
                {
                    params_t[i] = ((ITypeDefOrRef)vika.module.ResolveToken(reader.ReadInt32())).ToTypeSig(); ;
                    z += 4;
                }
            }
            var ret_tok = reader.ReadInt32();
            z += 4;
            TypeSig rt = null;
            if (ret_tok == 0)
            {
                rt = vika.module.CorLibTypes.Void;
            }
            else
            {
                rt = ((ITypeDefOrRef)vika.module.ResolveToken(ret_tok)).ToTypeSig();
            }
            x = z;

            if (!vika.all_devirts.ContainsKey(one + z))
            {

                MethodDefUser meth = new MethodDefUser("_ex_Call_" + offset.ToString(), MethodSig.CreateStatic(rt, params_t), MethodImplAttributes.IL, MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig);
                foreach (var type in params_t)
                    meth.ParamDefs.Add(new ParamDefUser( ));
                meth.Body = new CilBody();
                meth.Parameters.UpdateParameterTypes();



                vika.module.GlobalType.Methods.Add(meth);

                vika.devirt_methods_stack.Push(new Tuple<int, MethodDef>( one + z, meth));
                vika.all_devirts.Add(one + z, meth);

                return new Vika_Instruction(OpCodes.Call, meth);
            }
            else
            {
                return new Vika_Instruction(OpCodes.Call, vika.all_devirts[one + z]);


            }
        }

        List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
           OpCodes.Ldarg,
            OpCodes.Call,
           OpCodes.Callvirt,

             OpCodes.Ldc_I4,
           OpCodes.Call,
            OpCodes.Stloc,
           OpCodes.Ldloc,

               OpCodes.Brfalse,
           OpCodes.Ldarg,
                      OpCodes.Ldloc,


               OpCodes.Call,
           OpCodes.Ret,




        };
        private int x;

        public override bool nop_before()
        {
            return true;
        }

        public override bool is_specially()
        {
            return false;
        }
    }
}
