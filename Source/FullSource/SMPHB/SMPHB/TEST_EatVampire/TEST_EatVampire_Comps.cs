using RimWorld;

namespace SMPHB.TEST_EatVampire
{
    public class TEST_EatVampire_Comps : CompProperties_AbilityBloodfeederBite
    {
        public ThoughtDef thoughtDefToGiveTargetBeforeBite;

        public ThoughtDef opinionThoughtDefToGiveTargetBeforeBite;

        public TEST_EatVampire_Comps()
        {
            compClass = typeof(TEST_EatVampire_CompClass);
        }
    }
}
