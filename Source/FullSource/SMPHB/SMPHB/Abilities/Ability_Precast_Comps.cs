using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace SMPHB.Abilities
{
    public class Ability_Precast_Comps : CompProperties_AbilityEffect
    {
        public float summaryHemogenCost = -1f;
        public List<int> precastSeconds = new List<int>();
    }
}
