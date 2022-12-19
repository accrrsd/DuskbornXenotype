using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace SMPHB.Need_HemogenNeeded
{
    public class Need_SMPHB_Hemogen : Need
    {
        [Unsaved(false)]
        private Gene_Hemogen cachedHemogenGene;

        public Need_SMPHB_Hemogen(Pawn newPawn)
            : base(newPawn)
        {
        }

        public Color fillingColor = new ColorInt(145, 42, 42).ToColor;

        private const float MinAgeForNeed = 3f;

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

        public override bool ShowOnNeedList
        {
            get
            {
                if (pawn.ageTracker.AgeBiologicalYears < MinAgeForNeed)
                {
                    return false;
                }
                return base.ShowOnNeedList;
            }
        }

        public bool Starving => CurCategory == HungerCategory.Starving;

        public float HungryValue => 0.4f;

        public float UrgentlyHungryValue => 0.2f;

        private float MalnutritionSeverityPerInterval => 0.00113333331f * Mathf.Lerp(0.8f, 1.2f, Rand.ValueSeeded(pawn.thingIDNumber ^ 0x26EF7A));

        public override float MaxLevel => (float)(Hemogen != null ? Hemogen.Max : 1);

        public override int GUIChangeArrow => -1;

        public HungerCategory CurCategory
        {
            get
            {
                if (CurLevel <= 0f)
                {
                    return HungerCategory.Starving;
                }
                if (CurLevel < UrgentlyHungryValue)
                {
                    return HungerCategory.UrgentlyHungry;
                }
                if (CurLevel < HungryValue)
                {
                    return HungerCategory.Hungry;
                }
                return HungerCategory.Fed;
            }
        }

        protected override bool IsFrozen
        {
            get
            {
                if (!base.IsFrozen)
                {
                    return pawn.Deathresting;
                }
                return true;
            }
        }

        public override void NeedInterval()
        {
            if (!IsFrozen)
            {
                CurLevelPercentage = Hemogen.ValuePercent;

            }
            if (!IsFrozen || pawn.Deathresting)
            {
                if (Starving)
                {
                    HealthUtility.AdjustSeverity(pawn, HediffDefOflocal.SMPHB_Hemogen_Malnutrition, MalnutritionSeverityPerInterval);
                }
                else
                {
                    HealthUtility.AdjustSeverity(pawn, HediffDefOflocal.SMPHB_Hemogen_Malnutrition, 0f - MalnutritionSeverityPerInterval);
                }
            }
        }

        public override void DrawOnGUI(Rect rect, int maxThresholdMarkers = int.MaxValue, float customMargin = -1, bool drawArrows = true, bool doTooltip = true, Rect? rectForTooltip = null, bool drawLabel = true)
        {
            if (threshPercents == null)
            {
                threshPercents = new List<float>();
            }
            threshPercents.Clear();
            for (int i = 1; i < MaxLevel*100; i++)
            {
                if (i % 25 == 0)
                {
                    threshPercents.Add(1 / (MaxLevel*100 / i));
                }
            }
            //-------------------
            if (rect.height > 70f)
            {
                float num = (rect.height - 70f) / 2f;
                rect.height = 70f;
                rect.y += num;
            }
            Rect rect2 = rectForTooltip ?? rect;
            if (Mouse.IsOver(rect2))
            {
                Widgets.DrawHighlight(rect2);
            }
            if (doTooltip && Mouse.IsOver(rect2))
            {
                TooltipHandler.TipRegion(rect2, new TipSignal(() => GetTipString(), rect2.GetHashCode()));
            }
            float num2 = 14f;
            float num3 = ((customMargin >= 0f) ? customMargin : (num2 + 15f));
            if (rect.height < 50f)
            {
                num2 *= Mathf.InverseLerp(0f, 50f, rect.height);
            }
            if (drawLabel)
            {
                Text.Font = ((rect.height > 55f) ? GameFont.Small : GameFont.Tiny);
                Text.Anchor = TextAnchor.LowerLeft;
                Widgets.Label(new Rect(rect.x + num3 + rect.width * 0.1f, rect.y, rect.width - num3 - rect.width * 0.1f, rect.height / 2f), LabelCap);
                Text.Anchor = TextAnchor.UpperLeft;
            }
            Rect rect3 = rect;
            if (drawLabel)
            {
                rect3.y += rect.height / 2f;
                rect3.height -= rect.height / 2f;
            }
            rect3 = new Rect(rect3.x + num3, rect3.y, rect3.width - num3 * 2f, rect3.height - num2);
            if (DebugSettings.ShowDevGizmos)
            {
                float lineHeight = Text.LineHeight;
                Rect rect4 = new Rect(rect3.xMax - lineHeight, rect3.y - lineHeight, lineHeight, lineHeight);
                if (Widgets.ButtonImage(rect4.ContractedBy(4f), TexButton.Plus))
                {
                    GeneResourceDrainUtility.OffsetResource(Hemogen, +0.1f);
                }
                if (Mouse.IsOver(rect4))
                {
                    TooltipHandler.TipRegion(rect4, "+ 10");
                }
                Rect rect5 = new Rect(rect4.xMin - lineHeight, rect3.y - lineHeight, lineHeight, lineHeight);
                if (Widgets.ButtonImage(rect5.ContractedBy(4f), TexButton.Minus))
                {
                    GeneResourceDrainUtility.OffsetResource(Hemogen, -0.1f);
                }
                if (Mouse.IsOver(rect5))
                {
                    TooltipHandler.TipRegion(rect5, "- 10");
                }
            }
            Rect rect6 = rect3;
            float num4 = 1f;
            if (def.scaleBar && MaxLevel < 1f)
            {
                num4 = MaxLevel;
            }
            rect6.width *= num4;
            Rect barRect = Widgets.FillableBar(rect6, CurLevelPercentage, SolidColorMaterials.NewSolidColorTexture(fillingColor));
            if (drawArrows)
            {
                Widgets.FillableBarChangeArrows(rect6, GUIChangeArrow);
            }
            if (threshPercents != null)
            {
                for (int i = 0; i < Mathf.Min(threshPercents.Count, maxThresholdMarkers); i++)
                {
                    DrawBarThreshold(barRect, threshPercents[i] * num4);
                }
            }
            float curInstantLevelPercentage = CurInstantLevelPercentage;
            if (curInstantLevelPercentage >= 0f)
            {
                DrawBarInstantMarkerAt(rect3, curInstantLevelPercentage * num4);
            }
            if (!def.tutorHighlightTag.NullOrEmpty())
            {
                UIHighlighter.HighlightOpportunity(rect, def.tutorHighlightTag);
            }
            Text.Font = GameFont.Small;
        }
    }
}
