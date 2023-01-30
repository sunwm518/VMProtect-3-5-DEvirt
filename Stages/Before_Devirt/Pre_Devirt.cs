using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Before_Devirt
{
    class Pre_Devirt : IStage
    {
        public override void Run(Vika_Context vika)
        {
            vika.logger.info("Pre Devirt Stage Start!");

            Pre_Devirt_Context pre_ctx = new Pre_Devirt_Context();
            pre_ctx.logger = vika.logger;
            var stages = typeof(Program).Assembly.GetTypes().Where(sd => sd.BaseType == typeof(IPreDevirtStage));
            Dictionary<int, Type> types = new Dictionary<int, Type>();
            foreach (var step in stages)
            {
                var priority = step.GetMethods().Single(name => name.Name == "stage_number");
                types.Add((int)priority.Invoke(null, null), step);
            }
            types = types.OrderBy(sd => sd.Key).ToDictionary(x => x.Key, x => x.Value);
            foreach (var step in types)
            {
                vika.logger.info("Loadded Pre Divirt Stage: " + step.Value.Name);
            }
            foreach (var step in types)
            {
                var runner_type = (IPreDevirtStage)Activator.CreateInstance(step.Value);
                runner_type.Run(pre_ctx, vika.module);
            }
            vika.pre_context = pre_ctx;
            vika.logger.info("Pre Devirt Stage Completed!");



        }
        public static int stage_number() { return 1; }
        public override string who()
        {
            return "";
        }
    }
}
