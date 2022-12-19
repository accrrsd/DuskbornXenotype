using System.Collections.Generic;
using RimWorld;
using Verse;

namespace SMPHB.Abilities.GrabTheHeart
{
    public class GrabTheHeart_CompClass : CompAbilityEffect
    {
        HediffDef hediff;
        private Gene_Hemogen cachedHemogenGene;
        public new Ability_Precast_Comps Props => (Ability_Precast_Comps)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            HealthManager(target);
        }

        private Gene_Hemogen Hemogen
        {
            get
            {
                if (cachedHemogenGene == null)
                {
                    cachedHemogenGene = parent.pawn.genes?.GetFirstGeneOfType<Gene_Hemogen>();
                }
                return cachedHemogenGene;
            }
        }

        //Единственный способ это создать несколько прекастов, которые будут выполняться последовательно.
        public override IEnumerable<PreCastAction> GetPreCastActions()
        {
            //Последний будет выполняться первым
            yield return new PreCastAction
            {
                action = delegate (LocalTargetInfo target, LocalTargetInfo dest)
                {
                    HealthManager(target);
                },
                ticksAwayFromCast = 60 * Props.precastSeconds[0]
            };

            yield return new PreCastAction
            {
                action = delegate (LocalTargetInfo target, LocalTargetInfo dest)
                {
                    HealthManager(target);
                },
                ticksAwayFromCast = 60 * Props.precastSeconds[1]
            };

            yield return new PreCastAction
            {
                action = delegate (LocalTargetInfo target, LocalTargetInfo dest)
                {
                    HealthManager(target);
                },
                ticksAwayFromCast = 60 * Props.precastSeconds[2]
            };

            yield return new PreCastAction
            {
                action = delegate (LocalTargetInfo target, LocalTargetInfo dest)
                {
                    HealthManager(target);
                },
                ticksAwayFromCast = 60 * Props.precastSeconds[3]
            };
        }

        void HealthManager(LocalTargetInfo target)
        {
            if (hediff == default)
            {
                hediff = DefDatabase<HediffDef>.GetNamed("HeartAttack");
            }
            GeneResourceDrainUtility.OffsetResource(Hemogen, -(Props.summaryHemogenCost / Props.precastSeconds.Count));
            HealthUtility.AdjustSeverity(target.Pawn, hediff, 0.2f);
        }
    }
}
