using System;
using System.Collections.Generic;
using System.Linq;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt.Disassembler.Specials
{
    class Handlers_Disasm
    {
        public static void disasm_leave(Vika_Method method, Vika_Context vika, List<Vika_Instruction> instrs, Vika_Instruction instr)
        {

            int count = instrs.Count;

            instr.offset = instrs[count - 2].offset;
            instrs.RemoveAt(count - 1);
            count = instrs.Count;

            instrs.RemoveAt(count - 1);

            int handler_end = vika.value_stack.Pop();
            int handler_len = vika.value_stack.Pop();


            if (method.end_handler.Count > 0)
            {
                var handler = method.end_handler.Peek();
                if (handler.unknown_offset_ehs < handler_len)
                {
                    method.end_handler.Pop();
                    method.handlers[method.handlers.IndexOf(handler)].handler_end = handler_end;
                }
                else
                {
                    //var handler_two = method.end_handler_second.Pop();
                    //method.handlers[method.handlers.IndexOf(handler_two)].handler_end = handler_end;

                }
            }
            else
            {
                if (method.end_handler_second.Count() > 0)
                {
                    var handler_two = method.end_handler_second.Pop();
                    method.handlers[method.handlers.IndexOf(handler_two)].handler_end = handler_end;


                }
                else
                {

                }
            }


            instr.target = handler_end;
            if (!vika.devirt_context.offsets_list.Contains(handler_end))
            {
                vika.devirt_context.offsets.push(handler_end);
            }

        }

        internal static void disasm_try(Vika_Method method, Vika_Context vika, List<Vika_Instruction> instrs, Vika_Instruction instr)
        {
            int count = instrs.Count;
            instr.offset = instrs[count - 1].offset;
            instrs.RemoveAt(count - 1);

            int try_len = vika.value_stack.Pop();
            var eh = method.handlers.Where(sd => (try_len == sd.try_len)).ToList();
            if (eh.Count != 0)
            {
                foreach (var e in eh)
                {
                    if (e.eh_type == 0)
                    {
                        method.end_handler.Push(method.handlers[method.handlers.IndexOf(e)]);

                    }
                    else
                    {
                        method.end_handler_second.Push(method.handlers[method.handlers.IndexOf(e)]);

                    }
                    method.handlers[method.handlers.IndexOf(e)].try_offset = instr.offset;

                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}