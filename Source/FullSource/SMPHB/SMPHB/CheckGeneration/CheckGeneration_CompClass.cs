using RimWorld;
using Verse;

namespace SMPHB.CheckGeneration
{
    public class CheckGeneration_CompClass : CompAbilityEffect
    {
        public new CheckGeneration_Comps Props => (CheckGeneration_Comps)props;
        public override bool ShouldHideGizmo => Props.hideGizmo? HasGenerationProblem.value : base.ShouldHideGizmo;
        private (bool value, string reason) HasGenerationProblem
        {
            get
            {
                Hediff generation = parent.pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOflocal.SMPHB_PowerOfBloodline);
                if (generation != default)
                {
                    int severity = (int)generation.Severity;

                    if (severity <= Props.maxGeneration && severity >= Props.minGeneration)
                    {
                        return (false, null);
                    }
                    else if (severity > Props.maxGeneration)
                    {
                        return (true, "Generation greater then maximum");
                    }
                    else
                    {
                        return (true, "Generation less then minimal");

                    }
                }
                else
                {
                    return (true, "Has no generation");
                }
            }
        }

        public override bool GizmoDisabled(out string reason)
        {
            reason = HasGenerationProblem.reason;
            return HasGenerationProblem.value;
        }
    }
}
