using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace SMPHB.Addition_SeverityFromHemogen
{
    public class SeverityFromHemogen_CompClass : HediffComp_SeverityFromHemogen
    {
        public override bool CompShouldRemove => Main.TryToFindGene(Pawn) == null;
    }
}
