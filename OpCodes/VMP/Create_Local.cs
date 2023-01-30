﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.VMP
{
    class Create_Local : IOpCode
    {
        public override bool is_opcode(MethodDef md)
        {
            List<OpCode> codes_orig = new List<OpCode>();
            md.Body.SimplifyMacros(null);
            foreach (var instr in md.Body.Instructions)
            {
                codes_orig.Add(instr.OpCode);
            }
            if (codes_orig.Count >= pattern.Count)
                for (int i = 0; i < pattern.Count; i++)
                {
                    if (codes_orig[i] != pattern[i]) return false;
                }
            else
            {
                return false;
            }



            return true;
        }
        public override bool nop_before()
        {
            return true;
        }

        public override bool is_specially()
        {
            return false;
        }
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            var token = vika.value_stack.Pop();
            var type = (ITypeDefOrRef)vika.module.ResolveToken(Convert.ToUInt32( token));
            var local = md.method.Body.Variables.Add(new Local( type.ToTypeSig(),"Vika", md.method.Body.Variables.Count));
            return new Vika_Instruction(OpCodes.Stloc, local);
            
        }

        public override int Op_Size()
        {
            return 0;
        }

        List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
            OpCodes.Ldarg,
            OpCodes.Call,
             OpCodes.Callvirt,

             OpCodes.Call,
            OpCodes.Stloc,
            OpCodes.Ldarg,
             OpCodes.Call,

             OpCodes.Stloc,
            OpCodes.Ldarg,
            OpCodes.Ldarg,
             OpCodes.Call,

             OpCodes.Callvirt,
            OpCodes.Ldloc,
            OpCodes.Call,
             OpCodes.Stloc,



        };
    }
}
