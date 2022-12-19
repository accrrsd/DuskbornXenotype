using System.Collections.Generic;
using RimWorld;
using Verse;

namespace SMPHB.TEST_EatVampire
{
    public class TEST_EatVampire_CompClass : CompAbilityEffect_BloodfeederBite
    {
        public new TEST_EatVampire_Comps Props => (TEST_EatVampire_Comps)props;
        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            if (target.Pawn != null)
            {
                Hediff targetHediff = target.Pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOflocal.SMPHB_PowerOfBloodline);
                Hediff parentHediff = parent.pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOflocal.SMPHB_PowerOfBloodline);

                if (targetHediff.Severity < parentHediff.Severity)
                {
                    parentHediff.Severity = targetHediff.Severity;
                }
                else
                {
                    //Тут будем выдавать повышение настроения и оффсет способностей
                    parent.pawn.needs?.mood?.thoughts?.memories?.TryGainMemory((Thought_Memory)ThoughtMaker.MakeThought(ThoughtDefOfLocal.SMPHB_EatWeakBlood), target.Pawn);
                    
                }
            }
        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            bool validationPass = base.Valid(target, throwMessages);
            if (validationPass)
            {
                if (target.Pawn.health.hediffSet.HasHediff(HediffDefOflocal.SMPHB_PowerOfBloodline))
                {
                    return true;
                }
                else
                {
                    if (throwMessages)
                    {
                        Messages.Message("SMPHB_NotABloodlineInstance".Translate(parent.def.Named("ABILITY"), parent.pawn.Named("PAWN")), target.Pawn, MessageTypeDefOf.RejectInput, historical: false);
                    }
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool TryGiveMentalState(MentalStateDef def, Pawn p, Pawn caster)
        {
            bool attemptMentalState = p.mindState.mentalStateHandler.TryStartMentalState(def, reason: "SMPHB_TryedToKillMe".Translate(caster.Named("PAWN")), forceWake: true, causedByMood: false, null, transitionSilently: false, causedByDamage: false);
            if (attemptMentalState)
            {
                p.mindState.mentalStateHandler.CurState.forceRecoverAfterTicks = 60 * 60 * 2;
                p.mindState.mentalStateHandler.CurState.sourceFaction = caster.Faction;
            }
            return attemptMentalState;
        }

        public override IEnumerable<PreCastAction> GetPreCastActions()
        {
            yield return new PreCastAction
            {
                action = delegate (LocalTargetInfo target, LocalTargetInfo dest)
                {

                    if (TryGiveMentalState(MentalStateDefOf.Berserk, target.Pawn, parent.pawn))
                    {
                        if (Props.thoughtDefToGiveTargetBeforeBite != null)
                        {
                            target.Pawn.needs?.mood?.thoughts?.memories?.TryGainMemory((Thought_Memory)ThoughtMaker.MakeThought(Props.thoughtDefToGiveTargetBeforeBite), parent.pawn);
                        }
                        if (Props.opinionThoughtDefToGiveTargetBeforeBite != null)
                        {
                            target.Pawn.needs?.mood?.thoughts?.memories?.TryGainMemory((Thought_MemorySocial)ThoughtMaker.MakeThought(Props.opinionThoughtDefToGiveTargetBeforeBite), parent.pawn);
                        }
                    }

                },
                ticksAwayFromCast = 60 * (int)parent.def.verbProperties.warmupTime / 2
            };
        }
    }
}