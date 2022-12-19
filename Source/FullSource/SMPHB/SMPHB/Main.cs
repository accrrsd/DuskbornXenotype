using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Verse;

namespace SMPHB
{
    public class Main
    {
        public static Dictionary<int, float> hemogenMaxByGenerationList = new Dictionary<int, float>{
            {3,  5.5f},
            {4,  3.5f},
            {5,  3.0f},

            {6,  2.5f},
            {7,  2.2f},
            {8,  1.9f},

            {9,  1.6f},
            {10,  1.4f},

            {11,  1.2f},
            {12,  1.1f},
            {13,  1.0f},

            {14,  0.8f},
            {15, 0.7f},
            {16, 0.6f},
        };
        public static Gene TryToFindGene(Pawn pawn)
        {
            return pawn.genes?.GetGene(GeneDefOfLocal.SMPHB_Duskborn_Gene);
        }

        public static GeneDef TryToFindGeneDef(Pawn pawn)
        {
            List<Gene> genesListForReading = pawn.genes.GenesListForReading;
            for (int i = 0; i < genesListForReading.Count; i++)
            {
                GeneDef currentGene = genesListForReading[i].def;
                if (currentGene == GeneDefOfLocal.SMPHB_Duskborn_Gene)
                {
                    return GeneDefOfLocal.SMPHB_Duskborn_Gene;
                }
            }
            return null;
        }
    }
}

//return pawn.genes?.GetGene(GeneDefOfLocal.SMPHB_VentrueBloodline) ??
//    pawn.genes?.GetGene(GeneDefOfLocal.SMPHB_NosferatuBloodline) ??
//    pawn.genes?.GetGene(GeneDefOfLocal.SMPHB_TremereBloodline) ??
//    pawn.genes?.GetGene(GeneDefOfLocal.SMPHB_HecataBloodline);


//public static GeneDef TryToFindGeneDef(Pawn pawn)
//{
//    List<Gene> genesListForReading = pawn.genes.GenesListForReading;
//    for (int i = 0; i < genesListForReading.Count; i++)
//    {
//        GeneDef currentGene = genesListForReading[i].def;
//        if (currentGene == GeneDefOfLocal.SMPHB_VentrueBloodline)
//        {
//            return GeneDefOfLocal.SMPHB_VentrueBloodline;
//        }
//        else if (currentGene == GeneDefOfLocal.SMPHB_NosferatuBloodline)
//        {
//            return GeneDefOfLocal.SMPHB_NosferatuBloodline;
//        }
//        else if (currentGene == GeneDefOfLocal.SMPHB_TremereBloodline)
//        {
//            return GeneDefOfLocal.SMPHB_TremereBloodline;
//        }
//        else if (currentGene == GeneDefOfLocal.SMPHB_HecataBloodline)
//        {
//            return GeneDefOfLocal.SMPHB_HecataBloodline;
//        }
//    }
//    return null;
//}