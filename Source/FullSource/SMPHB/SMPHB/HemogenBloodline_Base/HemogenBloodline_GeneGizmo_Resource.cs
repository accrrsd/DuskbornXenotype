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
    [StaticConstructorOnStartup]
    public class HemogenBloodline_GeneGizmo_Resource : GeneGizmo_ResourceHemogen
    {
        //private Texture2D barTex;

        //private Texture2D barHighlightTex;

        //private static bool draggingBar;

        //private float targetValuePct;

        //private static readonly Texture2D EmptyBarTex = SolidColorMaterials.NewSolidColorTexture(GenUI.FillableBar_Empty);

        //private static readonly Texture2D ResourceTargetTex = SolidColorMaterials.NewSolidColorTexture(Color.white);

        //private static readonly Texture2D HemogenCostTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.78f, 0.72f, 0.66f));

        //public HemogenBloodline_GeneGizmo_Resource(Gene_Resource gene, List<IGeneResourceDrain> drainGenes, Color barColor, Color barHighlightColor) : base(gene, drainGenes, barColor, barHighlightColor)
        //{
        //    this.gene = gene;
        //    this.drainGenes = drainGenes;
        //    barTex = SolidColorMaterials.NewSolidColorTexture(barColor);
        //    barHighlightTex = SolidColorMaterials.NewSolidColorTexture(barHighlightColor);
        //    Order = -100f;
        //    targetValuePct = Mathf.Clamp(gene.targetValue / gene.Max, 0f, gene.Max - gene.MaxLevelOffset);
        //    //-------------------
        //    draggableBar = true;
        //}

        //public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth, GizmoRenderParms parms)
        //{
        //    GizmoResult result = defaultGizmoOnGUI(topLeft, maxWidth, parms);
        //    float num = Mathf.Repeat(Time.time, 0.85f);
        //    float num2 = 1f;
        //    if (num < 0.1f)
        //    {
        //        num2 = num / 0.1f;
        //    }
        //    else if (num >= 0.25f)
        //    {
        //        num2 = 1f - (num - 0.25f) / 0.6f;
        //    }
        //    if (((MainTabWindow_Inspect)MainButtonDefOf.Inspect.TabWindow)?.LastMouseoverGizmo is Command_Ability command_Ability && gene.Max != 0f)
        //    {
        //        foreach (CompAbilityEffect effectComp in command_Ability.Ability.EffectComps)
        //        {
        //            if (effectComp is CompAbilityEffect_HemogenCost compAbilityEffect_HemogenCost && compAbilityEffect_HemogenCost.Props.hemogenCost > float.Epsilon)
        //            {
        //                Rect rect = barRect.ContractedBy(3f);
        //                float width = rect.width;
        //                float num3 = gene.Value / gene.Max;
        //                rect.xMax = rect.xMin + width * num3;
        //                float num4 = Mathf.Min(compAbilityEffect_HemogenCost.Props.hemogenCost / gene.Max, 1f);
        //                rect.xMin = Mathf.Max(rect.xMin, rect.xMax - width * num4);
        //                GUI.color = new Color(1f, 1f, 1f, num2 * 0.7f);
        //                GenUI.DrawTextureWithMaterial(rect, HemogenCostTex, null);
        //                GUI.color = Color.white;
        //                return result;
        //            }
        //        }
        //        return result;
        //    }
        //    return result;
        //}

        //public GizmoResult defaultGizmoOnGUI(Vector2 topLeft, float maxWidth, GizmoRenderParms parms)
        //{
        //    Rect rect = new Rect(topLeft.x, topLeft.y, GetWidth(maxWidth), 75f);
        //    Rect rect2 = rect.ContractedBy(8f);
        //    Widgets.DrawWindowBackground(rect);
        //    Text.Font = GameFont.Small;
        //    Rect labelRect = rect2;
        //    labelRect.height = Text.LineHeight;
        //    bool mouseOverAnyHighlightableElement = false;
        //    DrawLabel(labelRect, ref mouseOverAnyHighlightableElement);
        //    rect2.yMin += labelRect.height + 8f;
        //    barRect = rect2;

        //    Widgets.FillableBar(barRect, gene.ValuePercent, barTex, EmptyBarTex, doBorder: true);
        //    if (!gene.def.resourceGizmoThresholds.NullOrEmpty())
        //    {
        //        for (int i = 0; i < gene.def.resourceGizmoThresholds.Count; i++)
        //        {
        //            float num = gene.def.resourceGizmoThresholds[i];
        //            Rect position = default(Rect);
        //            position.x = barRect.x + 3f + (barRect.width - 8f) * num;
        //            position.y = barRect.y + barRect.height - 9f;
        //            position.width = 2f;
        //            position.height = 6f;
        //            GUI.DrawTexture(position, (gene.Value < num) ? BaseContent.GreyTex : BaseContent.BlackTex);
        //        }
        //    }

        //    Widgets.DraggableBar(barRect, barTex, barHighlightTex, EmptyBarTex, ResourceTargetTex, ref draggingBar, gene.ValuePercent, ref targetValuePct, gene.def.resourceGizmoThresholds, gene.MaxForDisplay / 10);
        //    targetValuePct = Mathf.Clamp(targetValuePct, 0f, (gene.Max - gene.MaxLevelOffset) / gene.Max);
        //    gene.SetTargetValuePct(targetValuePct);

        //    int valueForDisplay = gene.ValueForDisplay;
        //    string label = string.Concat(arg2: gene.MaxForDisplay, arg0: valueForDisplay, arg1: " / ");
        //    Text.Anchor = TextAnchor.MiddleCenter;
        //    Widgets.Label(barRect, label);
        //    Text.Anchor = TextAnchor.UpperLeft;
        //    if (Mouse.IsOver(rect) && !mouseOverAnyHighlightableElement)
        //    {
        //        Widgets.DrawHighlight(rect);
        //        string tip = GetTooltip();
        //        if (!tip.NullOrEmpty())
        //        {
        //            TooltipHandler.TipRegion(rect, () => tip, Gen.HashCombineInt(gene.GetHashCode(), 17626387));
        //        }
        //    }
        //    return new GizmoResult(GizmoState.Clear);
        //}
        public HemogenBloodline_GeneGizmo_Resource(Gene_Resource gene, List<IGeneResourceDrain> drainGenes, Color barColor, Color barhighlightColor) : base(gene, drainGenes, barColor, barhighlightColor)
        {
            draggableBar = true;
        }
    }
}
