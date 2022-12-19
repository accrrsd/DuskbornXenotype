using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;

namespace SMPHB.TEST_EatVampire
{
    public class TEST_EatVampire_VerbClass : Verb_CastAbilityTouch
    {
        protected override bool TryCastShot()
        {
            //TryGiveMentalState(MentalStateDefOf.Berserk, currentTarget.Pawn. parent.pawn);

            currentTarget.Pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false);
            currentTarget.Pawn.mindState.mentalStateHandler.CurState.forceRecoverAfterTicks = 60 * 60 * 2;


            return base.TryCastShot();
        }
    }
}
