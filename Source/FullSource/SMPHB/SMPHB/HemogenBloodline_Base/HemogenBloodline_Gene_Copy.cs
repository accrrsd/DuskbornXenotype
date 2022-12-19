using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RimWorld;
using UnityEngine;
using Verse;
using Random = System.Random;

namespace SMPHB.HemogenBloodline_Base
{
    public class HemogenBloodline_Gene : Gene_HemogenDrain
    {

        //План действий:
        //НУЖНО : Заменить resourceGizmoType у гена, не меняя их у всех остальных генов от того же дефа.
        //ЛИБО: Можно пропатчить игру так, чтобы всунуть туда проверку на гены из моего мода (плохой способ)

        private Gene_Hemogen cachedHemogenGene;

        readonly HediffDef PowerOfBloodLine = HediffDefOflocal.SMPHB_PowerOfBloodline;
        //private GeneDef CopyOfHemogenDef
        //{
        //    get
        //    {
        //        GeneDef localCopy = Main.DeepCopy(Hemogen?.def) ?? cachedHemogenGene?.def;
        //        localCopy.defName = "randomHemogen123";
        //        localCopy.resourceGizmoType = typeof(HemogenBloodline_GeneGizmo_Resource);
        //        return localCopy;
        //    }
        //}

        private Gene_Hemogen Hemogen
        {
            get
            {
                if (cachedHemogenGene == null)
                {
                    cachedHemogenGene = pawn.genes?.GetFirstGeneOfType<Gene_Hemogen>();
                }
                return cachedHemogenGene;
            }
        }

        private new Gene_Resource Resource => Hemogen.Resource;

        //Кривая шанса выдачи
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

        private List<IGeneResourceDrain> tmpDrainGenes = new List<IGeneResourceDrain>();
        private List<IGeneResourceDrain> DrainGenes
        {
            get
            {
                tmpDrainGenes.Clear();
                List<Gene> genesListForReading = pawn.genes.GenesListForReading;
                for (int i = 0; i < genesListForReading.Count; i++)
                {
                    if (genesListForReading[i] is IGeneResourceDrain geneResourceDrain && geneResourceDrain.Resource == Resource)
                    {
                        tmpDrainGenes.Add(geneResourceDrain);
                    }
                }
                return tmpDrainGenes;
            }
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

        public override IEnumerable<Gizmo> GetGizmos()
        {
            yield return (GeneGizmo_Resource)Activator.CreateInstance(def.resourceGizmoType, Resource, DrainGenes, new ColorInt(138, 3, 3).ToColor, new ColorInt(145, 42, 42).ToColor);

            //Создает DEV кнопку для drain
            base.GetGizmos();
        }

        //public override IEnumerable<Gizmo> GetGizmos()
        //{

        //    if (Active)
        //    {
        //        CopyOfHemogenDef.resourceGizmoThresholds.Clear();
        //        CopyOfHemogenDef.resourceGizmoThresholds.Add(0.5f);
        //        CopyOfHemogenDef.resourceGizmoThresholds.Add(0.75f);
        //        CopyOfHemogenDef.resourceGizmoThresholds.Add(1f);
        //        CopyOfHemogenDef.resourceGizmoThresholds.Add(1.25f);
        //        CopyOfHemogenDef.resourceGizmoThresholds.Add(1.5f);

        //        //GeneGizmo_ResourceHemogen newGizmo = typeof (GeneGizmo_ResourceHemogen);


        //        yield return (GeneGizmo_Resource)Activator.CreateInstance(CopyOfHemogenDef.resourceGizmoType, Hemogen, null, new ColorInt(138, 3, 3).ToColor, new ColorInt(145, 42, 42).ToColor);
        //    }

        //    foreach (Gizmo gizmo in base.GetGizmos())
        //    {
        //        yield return gizmo;
        //    }

        //}

        public override void Tick()
        {
            base.Tick();
            pawn.needs.food.CurLevelPercentage = 1;

            //if (Pawn.IsHashIntervalTick(60))
            //{
            //    //ManageGizmoThresholds();
            //}


        }

        //Отключаем кормежку от мяса
        public override void Notify_IngestedThing(Thing thing, int numTaken)
        {

        }
    }
}
