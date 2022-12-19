using static SMPHB.Main;
using RimWorld;
using Verse;

namespace SMPHB.TEST_GiveVampire
{
    public class TEST_GiveVampire_CompClass : CompAbilityEffect
    {
        readonly HediffDef PowerOfBloodLine = HediffDefOflocal.SMPHB_PowerOfBloodline;

        (Hediff parentBloodline, GeneDef parentBloodlineGene, bool valid) ParentValidationInfo
        {
            get
            {
                GeneDef parentBloodlineGene = TryToFindGeneDef(parent.pawn);
                Hediff parentBloodline = parent.pawn.health.hediffSet.GetFirstHediffOfDef(PowerOfBloodLine);
                return (parentBloodline, parentBloodlineGene, parentBloodline != null && parentBloodlineGene != null);
            }
        }
        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            if (target.Pawn != null)
            {
                if (ParentValidationInfo.valid)
                {
                    float parentSeverity = ParentValidationInfo.parentBloodline.Severity;
                    Hediff targetPower = HediffMaker.MakeHediff(HediffDefOflocal.SMPHB_PowerOfBloodline, target.Pawn);
                    targetPower.Severity = parentSeverity + 1;
                    target.Pawn.health.AddHediff(targetPower);

                    if (!target.Pawn.genes.HasGene(GeneDefOf.Hemogenic))
                    {
                        target.Pawn.genes.AddGene(GeneDefOf.Hemogenic, true);
                    }
                    else
                    {
                        Gene bloodFeed = target.Pawn.genes.GetGene(GeneDefOf.Bloodfeeder);
                        if (bloodFeed != null)
                        {
                            target.Pawn.genes.RemoveGene(bloodFeed);
                        }
                    }
                    target.Pawn.genes.AddGene(ParentValidationInfo.parentBloodlineGene, true);
                }
            }
        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            bool validationPass = base.Valid(target, throwMessages);
            if (target.Pawn != null && validationPass)
            {
                if (!target.Pawn.health.hediffSet.HasHediff(HediffDefOflocal.SMPHB_PowerOfBloodline))
                {
                    if (ParentValidationInfo.valid)
                    {
                        return true;
                    }
                    else
                    {
                        if (throwMessages)
                        {
                            Messages.Message("Caster validation error: No HemogenPower or bloodline", parent.pawn, MessageTypeDefOf.RejectInput, historical: false);
                        }
                        return false;
                    }
                }
                else
                {
                    if (throwMessages)
                    {
                        Messages.Message("SMPHB_TargetHasHemogenPowerHediff".Translate(), target.Pawn, MessageTypeDefOf.RejectInput, historical: false);
                    }
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}