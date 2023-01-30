using dnlib.DotNet.Emit;
using System.Collections.Generic;

namespace Vika_Anti_VMP.Helpers
{
    public class Logicals_Patterns_Special
    {
        public static List<OpCode> read_addr = new List<OpCode>() {
            OpCodes.Nop,
            OpCodes.Add,
            OpCodes.Ldc_I4,
            OpCodes.Shl,
            OpCodes.Ldc_I4,
        };
        public static List<OpCode> mem_prot = new List<OpCode>() {
            OpCodes.Add,
            OpCodes.Add,
            OpCodes.Nop,
        };
    }
    public class Logicals_Patterns_Opcodes
    {
        public static List<OpCode> clt = new List<OpCode>() {
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Shr_Un,
            OpCodes.Ldc_I4,

            OpCodes.Nop,
            OpCodes.Ldc_I4,
        };
        public static List<OpCode> clt_un = new List<OpCode>() {
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Shr_Un,
            OpCodes.Ldc_I4,

            OpCodes.Nop,
        };

        public static List<OpCode> cgt_un = new List<OpCode>() {
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Shr_Un,
            OpCodes.Ldc_I4,

            OpCodes.Add,
            OpCodes.Ldc_I4,
            OpCodes.Not,
            OpCodes.Nop,

       //     OpCodes.Nop,


        };
        public static List<OpCode> cgt = new List<OpCode>() {
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Shr_Un,
            OpCodes.Ldc_I4,

            OpCodes.Add,
            OpCodes.Ldc_I4,
            OpCodes.Not,
            OpCodes.Nop,

            OpCodes.Ldc_I4,


        };
        public static List<OpCode> ceq = new List<OpCode>() {
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,
            OpCodes.Nop,

            OpCodes.Ldc_I4,
           

        };
    }
    public class Logicals_Patterns_Branches
    {



        public static List<OpCode> Br = new List<OpCode>() {

            OpCodes.Ldc_I4,

        };


        public static List<OpCode> Beq = new List<OpCode>() {
            OpCodes.Add,
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,
            OpCodes.Dup,
            OpCodes.And,
            OpCodes.Ldc_I4,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,



              OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,

            OpCodes.Nop,
            OpCodes.Ldc_I4,


        };

        public static List<OpCode> Bge = new List<OpCode>() {
                    OpCodes.Add,
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,
            OpCodes.Dup,
            OpCodes.And,
            OpCodes.Ldc_I4,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,



              OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Shr_Un,

            OpCodes.Ldc_I4,
            OpCodes.Not,
                        OpCodes.Nop,
                                                                        OpCodes.Ldc_I4,




        };

        public static List<OpCode> Bge_Un = new List<OpCode>() {
            OpCodes.Add,
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,
            OpCodes.Dup,
            OpCodes.And,
            OpCodes.Ldc_I4,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,



              OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Shr_Un,

            OpCodes.Ldc_I4,
            OpCodes.Not,
                        OpCodes.Conv_I4,
                                                OpCodes.Nop,


        };

        public static List<OpCode> Bgt = new List<OpCode>() {
            OpCodes.Add,
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,
            OpCodes.Dup,
            OpCodes.And,
            OpCodes.Ldc_I4,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,



              OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Shr_Un,

            OpCodes.Ldc_I4,
            OpCodes.Add,
                        OpCodes.Ldc_I4,
                                                OpCodes.Not,
            OpCodes.Nop,
                        OpCodes.Ldc_I4,



        };

        public static List<OpCode> Bgt_Un = new List<OpCode>() {
            OpCodes.Add,
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,
            OpCodes.Dup,
            OpCodes.And,
            OpCodes.Ldc_I4,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,



              OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Shr_Un,

            OpCodes.Ldc_I4,
            OpCodes.Add,
                        OpCodes.Ldc_I4,
                                                OpCodes.Not,
            OpCodes.Conv_I4,
                        OpCodes.Nop,



        };

        public static List<OpCode> Ble = new List<OpCode>() {
            OpCodes.Add,
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,
            OpCodes.Dup,
            OpCodes.And,
            OpCodes.Ldc_I4,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,



              OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Shr_Un,

            OpCodes.Ldc_I4,
            OpCodes.Not,
                        OpCodes.Add,
                                                OpCodes.Ldc_I4,
            OpCodes.Nop,
                        OpCodes.Ldc_I4,



        };
        public static List<OpCode> Ble_Un = new List<OpCode>() {
            OpCodes.Add,
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,
            OpCodes.Dup,
            OpCodes.And,
            OpCodes.Ldc_I4,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,



              OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Shr_Un,

            OpCodes.Ldc_I4,
            OpCodes.Not,
                        OpCodes.Add,
                                                OpCodes.Ldc_I4,
            OpCodes.Conv_I4,
                        OpCodes.Nop,



        };
        public static List<OpCode> Blt = new List<OpCode>() {
            OpCodes.Add,
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,
            OpCodes.Dup,
            OpCodes.And,
            OpCodes.Ldc_I4,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,



              OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Shr_Un,

            OpCodes.Ldc_I4,
            OpCodes.Nop,
                                                OpCodes.Ldc_I4,


        };
        public static List<OpCode> Blt_Un = new List<OpCode>() {
            OpCodes.Add,
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,
            OpCodes.Dup,
            OpCodes.And,
            OpCodes.Ldc_I4,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,



              OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Shr_Un,

            OpCodes.Ldc_I4,
            OpCodes.Conv_I4,
                                                OpCodes.Nop,


        };

        public static List<OpCode> Bne_Un = new List<OpCode>() {
            OpCodes.Add,
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,
            OpCodes.Dup,
            OpCodes.And,
            OpCodes.Ldc_I4,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,



              OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Conv_I4,

            OpCodes.Nop,


        };
        public static List<OpCode> Brfalse = new List<OpCode>() {//
                    OpCodes.Add,

        OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,
            OpCodes.Dup,
            OpCodes.And,
            OpCodes.Ldc_I4,

            OpCodes.Not,
            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,




        };
        public static List<OpCode> Brtrue = new List<OpCode>() {
            OpCodes.Add,
            OpCodes.And,
            OpCodes.Ldc_I4,
            OpCodes.Not,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,
            OpCodes.Dup,
            OpCodes.And,
            OpCodes.Ldc_I4,

            OpCodes.Neg,
            OpCodes.Conv_I,
            OpCodes.Nop,




        };

    }
}