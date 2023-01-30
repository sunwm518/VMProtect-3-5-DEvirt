using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt
{
    public abstract class IDevirtStage
    {
      
            public abstract void Run(Devirt_Context ctx, Vika_Context vika);
            public abstract string who();

       
    }
}
