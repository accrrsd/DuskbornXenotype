using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;

namespace SMPHB.TEST_EatVampire
{
    public class TEST_EatVampire_CompProperties : CompProperties_AbilityBloodfeederBite
    {
        public new float targetBloodLoss = 1f;

        public TEST_EatVampire_CompProperties()
        {
            compClass = typeof(TEST_EatVampire_CompClass);
        }
    }
}
