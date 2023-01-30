using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vika_Anti_VMP_OOP_3._5_Based.Stages
{
    public abstract class IStage
    {
        public abstract void Run(Vika_Context ctx);
        public abstract string who();

    }
}
