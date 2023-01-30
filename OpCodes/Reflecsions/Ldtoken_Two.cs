//using dnlib.DotNet;
//using dnlib.DotNet.Emit;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;


//namespace Vika_Anti_VMP_OOP_3._5_Based.Reflecsions
//{
//    class Ldtoken_Two : IOpCode
//    {
//        public override bool nop_before()
//        {
//            return true;
//        }

//        public override bool is_specially()
//        {
//            return false;
//        }
//        public override bool is_opcode(MethodDef md)
//        {
//            List<OpCode> codes_orig = new List<OpCode>();
//            md.Body.SimplifyMacros(null);
//            foreach (var instr in md.Body.Instructions)
//            {
//                codes_orig.Add(instr.OpCode);
//            }
//            if (codes_orig.Count > pattern.Count)
//            {
//                for (int i = 0; i < codes_orig.Count; i++)
//                {
//                    try
//                    {
//                        if (codes_orig[i] != pattern[i]) return false;
//                    }
//                    catch
//                    {

//                    }
//                }
//            }
//            else
//            {
//                return false;
//            }

//            if (md.Body.Instructions[12].OpCode != OpCodes.Switch) return false;


//            return true;

//        }

//        public override int Op_Size()
//        {
//            return 0;
//        }
//        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
//        {




//            int token = Convert.ToInt32(vika.value_stack.Pop());
//            var tok_res = vika.module.ResolveToken(Convert.ToUInt32(token));

//            int num2;
//            do
//            {
//                num2 = token >> 24;
//                if (num2 <= 10)
//                {
//                    switch (num2)
//                    {
//                        case 1:
//                        case 2:
//                            continue;
//                        case 3:
//                        case 5:
//                            goto IL_13C;
//                        case 4:
//                            goto IL_FB;
//                        case 6:
//                            goto IL_142;
//                    }
//                    goto Block_2;
//                }
//            }
//            while (num2 == -50);
//            if (num2 != 109)
//            {
//                goto IL_13C;
//            }
//            goto IL_142;
//        Block_2:
//            if (num2 != 10)
//            {
//                goto IL_13C;
//            }
//            try
//            {
//                return new Vika_Instruction(OpCodes.Ldtoken, (IField)tok_res);
//            }
//            catch
//            {
//                return new Vika_Instruction(OpCodes.Ldtoken, (IMethodDefOrRef)tok_res);
//            }
//        IL_FB:
//            return new Vika_Instruction(OpCodes.Ldtoken, (IField)tok_res);
//        IL_13C:
//            return new Vika_Instruction(OpCodes.Nop);

//        IL_142:
//            return new Vika_Instruction(OpCodes.Ldtoken, (IMethodDefOrRef)tok_res);








//        }

//        List<OpCode> pattern = new List<OpCode>() {
//            OpCodes.Ldarg,
//            OpCodes.Call,
//            OpCodes.Callvirt,
//            OpCodes.Stloc,

//             OpCodes.Br,
//            OpCodes.Ldloc,
//            OpCodes.Ldc_I4,
//            OpCodes.Beq,


//            OpCodes.Br,



//        };
//    }
//}
