using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt
{
    class Devirt : IStage
    {
        public override void Run(Vika_Context vika)
        {
            vika.logger.info("Devirt Stage Start!");

            Devirt_Context ctx = new Devirt_Context();
            ctx.vika = vika;
            var stages = typeof(Program).Assembly.GetTypes().Where(sd => sd.BaseType == typeof(IDevirtStage));
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
                var runner_type = (IDevirtStage)Activator.CreateInstance(step.Value);
                runner_type.Run(ctx, vika);
            }
            vika.devirt_context = ctx;
            vika.logger.info("Devirt Stage Completed!");
        }

        public override string who()
        {
            throw new NotImplementedException();
        }
        public static int stage_number() { return 2; }

    }
}
