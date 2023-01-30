using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Interfaces;
using Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Code_Cleanup
{
    class Cleaner : IStage
    {
        public override void Run(Vika_Context vika)
        {
            vika.logger.info("Clean Stage Start!");       
            var stages = typeof(Program).Assembly.GetTypes().Where(sd => sd.BaseType == typeof(ICleaner));
            Dictionary<int, Type> types = new Dictionary<int, Type>();
            foreach (var step in stages)
            {
                var priority = step.GetMethods().Single(name => name.Name == "stage_number");
                types.Add((int)priority.Invoke(null, null), step);
            }
            types = types.OrderBy(sd => sd.Key).ToDictionary(x => x.Key, x => x.Value);
            foreach (var step in types)
            {
                vika.logger.info("Loadded Cleaner Stage: " + step.Value.Name);
            }
            foreach (var step in types)
            {
                var runner_type = (ICleaner)Activator.CreateInstance(step.Value);
                runner_type.Run(vika);
            }
            vika.logger.info("Clean Stage Completed!");
        }
        public static int stage_number() { return 3; }

        public override string who()
        {
            throw new NotImplementedException();
        }
    }
}
