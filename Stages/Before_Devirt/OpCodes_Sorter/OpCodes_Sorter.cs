using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using Vika_Anti_VMP.Helpers;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Before_Devirt.OpCodes_Sorter
{
    class OpCodes_Sorter : IPreDevirtStage
    {
        public override void Run(Pre_Devirt_Context vika, ModuleDefMD mod)
        {
            vika.all_opcodes = new List<Opcode_Context>();
            var opcodes = typeof(Program).Assembly.GetTypes().Where(sd => sd.BaseType == typeof(IOpCode));
            foreach (var step in opcodes)
            {
                vika.logger.debug("Loadded Vika realised_OpCode: " + step.Name);
            }

            foreach (var vmp_method in vika.vmp_opcodes_methods)
            {
                foreach (var vika_code in opcodes)
                {
                    var runner_type = (IOpCode)Activator.CreateInstance(vika_code);
                    Mutation_Cleaner.clean(vmp_method);

                    if (runner_type.is_opcode(vmp_method))
                    {
                        vika.logger.info(runner_type.GetType().Name + vmp_method.MDToken.ToInt32().ToString());
                        vmp_method.Name = runner_type.GetType().Name;

                        if (!vika.all_opcodes.Exists(sd => sd.vmp_method_opcode == vmp_method))
                        {

                            vika.all_opcodes.Add(new Opcode_Context(runner_type, vmp_method));
                        }
                        else
                        {

                            throw new Exception(vika.all_opcodes.Single(sd => sd.vmp_method_opcode == vmp_method).vika_opcode.GetType().Name);
                        }
                    }
                }
            }
            Console.ReadLine();
        }

        public override string who()
        {
            throw new NotImplementedException();
        }
        public static int stage_number() { return 3; }

    }
}
