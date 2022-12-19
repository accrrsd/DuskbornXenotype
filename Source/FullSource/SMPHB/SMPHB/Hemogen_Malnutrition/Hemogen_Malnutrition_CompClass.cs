using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace SMPHB.Hemogen_Malnutrition
{
    public class Hemogen_Malnutrition_CompClass : HediffComp, IGeneResourceDrain
    {
        Gene_Hemogen cachedHemogenGene;
        public Gene_Resource Resource
        {
            get
            {
                if (cachedHemogenGene == null || !cachedHemogenGene.Active)
                {
                    cachedHemogenGene = Pawn.genes.GetFirstGeneOfType<Gene_Hemogen>();
                }
                return cachedHemogenGene;
            }
        }

        public bool CanOffset => Resource != null && ((IGeneResourceDrain)Resource).CanOffset;

        public float ResourceLossPerDay => 0.1f;

        public string DisplayLabel => "Hemogen starving";

        public override void CompPostTick(ref float severityAdjustment)
        {
            GeneResourceDrainUtility.TickResourceDrain(this);
        }
    }
}
