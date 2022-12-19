using System;
using RimWorld;
using Verse;

namespace SMPHB.HemogenBloodline_Base
{
    public class HemogenBloodline_Gene : Gene_HemogenDrain, IGeneResourceDrain
    {
        readonly HediffDef PowerOfBloodLine = HediffDefOflocal.SMPHB_PowerOfBloodline;

        public int GivePowerOfbloodline()
        {
            int res;

            Random random = new Random();
            int randomRes = random.Next(0, 100);

            int Neonate = 60;
            int Ancilla = 20;
            int Elder = 10;
            int Methuselah = 5;

            if (randomRes <= Methuselah)
            {
                res = random.Next(4, 5);
            }
            else if (randomRes <= Elder)
            {
                res = random.Next(6, 8);
            }
            else if (randomRes <= Ancilla)
            {
                res = random.Next(9, 10);
            }
            else if (randomRes <= Neonate)
            {
                res = random.Next(11, 13);
            }
            else
            {
                res = random.Next(14, 16);
            }
            return res;
        }
        public override void PostAdd()
        {
            base.PostAdd();
            //Если пешку заражают, то ей дают сначала вампиризм, а потом ген, т.е если пешку заразили, то ген не дает случайный вампиризм.
            if (!Pawn.health.hediffSet.HasHediff(PowerOfBloodLine))
            {
                Hediff power = HediffMaker.MakeHediff(PowerOfBloodLine, pawn);
                power.Severity = GivePowerOfbloodline();
                pawn.health.AddHediff(power);
            }
        }

        public override void Tick()
        {
            base.Tick();
            if (pawn.IsHashIntervalTick(60))
            {
                if (pawn.needs.food != null)
                {
                    pawn.needs.food.CurLevelPercentage = 1f;
                }
            }
        }
    }
}
