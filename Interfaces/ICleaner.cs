using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Stages.Devirt;

namespace Vika_Anti_VMP_OOP_3._5_Based.Interfaces
{
    public abstract class ICleaner
    {
        public abstract void Run(Vika_Context vika);
        public abstract string who();

    }
}
