using RimWorld;
using Verse;

namespace SMPHB.Hediff_PowerOfBloodline
{
    public class PowerOfBloodline_CompClass : HediffComp
    {
        float cachedSeverity;
        Hediff cachedBloodlinePower;
        Gene_Hemogen cachedHemogenGene;

        private Gene_Hemogen Hemogen
        {
            get
            {
                if (cachedHemogenGene == null)
                {
                    cachedHemogenGene = Pawn.genes?.GetFirstGeneOfType<Gene_Hemogen>();
                }
                return cachedHemogenGene;
            }
        }

        private Hediff BloodlinePower
        {
            get
            {
                if (cachedBloodlinePower == default)
                {
                    cachedBloodlinePower = Pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOflocal.SMPHB_PowerOfBloodline);
                }
                return cachedBloodlinePower;
            }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (BloodlinePower != null)
            {
                if (cachedSeverity != default)
                {
                    if (cachedSeverity != BloodlinePower.Severity)
                    {
                        if (Hemogen != null)
                        {
                            Hemogen.SetMax(Main.hemogenMaxByGenerationList[(int)BloodlinePower.Severity]);
                            cachedSeverity = BloodlinePower.Severity;
                        }
                    }
                }
                else
                {
                    if (Hemogen != null)
                    {
                        Hemogen.SetMax(Main.hemogenMaxByGenerationList[(int)BloodlinePower.Severity]);
                        cachedSeverity = BloodlinePower.Severity;
                    }
                }
            }
        }
    }
}