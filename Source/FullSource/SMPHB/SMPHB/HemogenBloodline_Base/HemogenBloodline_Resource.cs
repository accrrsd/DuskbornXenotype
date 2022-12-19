using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using Verse;

namespace SMPHB.HemogenBloodline_Base
{
    public class HemogenBloodline_Resource : Gene_Resource
    {
        public override float InitialResourceMax => throw new NotImplementedException();

        public override float MinLevelForAlert => throw new NotImplementedException();

        protected override Color BarColor => throw new NotImplementedException();

        protected override Color BarHighlightColor => throw new NotImplementedException();

        protected new HemogenBloodline_GeneGizmo_Resource gizmo;

        private List<IGeneResourceDrain> tmpDrainGenes = new List<IGeneResourceDrain>();

        private List<IGeneResourceDrain> DrainGenes
        {
            get
            {
                tmpDrainGenes.Clear();
                List<Gene> genesListForReading = pawn.genes.GenesListForReading;
                for (int i = 0; i < genesListForReading.Count; i++)
                {
                    if (genesListForReading[i] is IGeneResourceDrain geneResourceDrain && geneResourceDrain.Resource == this)
                    {
                        tmpDrainGenes.Add(geneResourceDrain);
                    }
                }
                return tmpDrainGenes;
            }
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            if (Active)
            {
                if (gizmo == null)
                {
                    gizmo = (HemogenBloodline_GeneGizmo_Resource)Activator.CreateInstance(def.resourceGizmoType, this, DrainGenes, BarColor, BarHighlightColor);
                }
                yield return gizmo;
            }
        }
    }
}
