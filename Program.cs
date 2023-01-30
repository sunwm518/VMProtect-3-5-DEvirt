using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Stages;

namespace Vika_Anti_VMP_OOP_3._5_Based
{
    class Program
    {
        static void Main(string[] args)
        {

            var counter = 0;

         
            Vika_Context vika = new Vika_Context();
            vika.logger = new Console_Logger();
            Colorful.Console.WriteAscii("Vika__", System.Drawing.Color.DarkBlue);
            Colorful.Console.WriteLine("Vika_Anti_VMP_" + Vika_Config.version + "", System.Drawing.Color.Cyan);

            vika.logger.info(args[0]);
            vika.path = args[0];
            vika.module = ModuleDefMD.Load(args[0]);

            var stages = typeof(Program).Assembly.GetTypes().Where(sd => sd.BaseType == typeof(IStage));
            Dictionary<int, Type> types = new Dictionary<int, Type>();
            foreach(var step in stages)
            {
                var priority = step.GetMethods().Single(name => name.Name == "stage_number");
                types.Add((int)priority.Invoke(null, null),step);
            }
            types = types.OrderBy(sd => sd.Key).ToDictionary(x => x.Key, x => x.Value);
            foreach (var step in types)
            {
                vika.logger.info("Loadded Stage: " + step.Value.Name);
            }
            foreach (var step in types)
            {
                var runner_type =(IStage) Activator.CreateInstance( step.Value);
                runner_type.Run(vika);
            }

            vika.module.Write(args[0].Replace(".exe", "-vika_antis.exe"), new dnlib.DotNet.Writer.ModuleWriterOptions(vika.module) { Logger = DummyLogger.NoThrowInstance, MetadataOptions = new dnlib.DotNet.Writer.MetadataOptions() { Flags =  dnlib.DotNet.Writer.MetadataFlags.PreserveAll } });
            Console.ReadLine();
        }

        static StreamWriter wr = new StreamWriter("Disasm.txt");

        public class Console_Logger : ILogger
        {
            public override void debug(string message)
            {
                if(Vika_Config.is_debug)
                    wr.WriteLine("[DEBUG]::::" + message, System.Drawing.Color.Cyan);
            }
            public override void info(string message)
            {
                    Colorful.Console.WriteLine("[Info]::::" + message, System.Drawing.Color.Yellow);
            }

        }
    }
}
