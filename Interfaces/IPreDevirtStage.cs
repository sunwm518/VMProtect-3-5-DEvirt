using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages.Before_Devirt
{
    public abstract class IPreDevirtStage
    {
      
            public abstract void Run(Pre_Devirt_Context ctx, ModuleDefMD mod);
            public abstract string who();

       
    }
}
