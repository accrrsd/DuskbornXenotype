using Verse;

namespace SMPHB.CheckGeneration
{
    public class CheckGeneration_Comps : AbilityCompProperties
    {
        public int minGeneration;
        public int maxGeneration;
        public bool hideGizmo;

        public CheckGeneration_Comps()
        {
            compClass = typeof(CheckGeneration_CompClass);
        }
    }
}
