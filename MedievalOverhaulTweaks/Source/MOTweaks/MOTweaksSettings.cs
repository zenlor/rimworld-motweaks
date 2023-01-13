using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MOTweaks
{
    public class MOTweaksSettings : ModSettings
    {
        public bool genericPatches = false;
        public bool hygieneIrrigation = true;
        public bool hygieneHeating = false;
        public bool vfeClassicalIntegrations = true;
        public int rimFantasyWork = 2;
        public int rimFantasyProduct = 1800;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref genericPatches, "genericPatches", false);
            Scribe_Values.Look<bool>(ref hygieneIrrigation, "hygieneIrrigation", true);
            Scribe_Values.Look<bool>(ref hygieneHeating, "hygieneHeating", false);
            Scribe_Values.Look<bool>(ref vfeClassicalIntegrations, "vfeClassicalIntegrations", true);
            Scribe_Values.Look<int>(ref rimFantasyProduct, "rimFantasyProduct", 10);
            Scribe_Values.Look<int>(ref rimFantasyWork, "rimFantasyWork", 800);
        }

        public IEnumerable<string> GetEnabledSettings
        {
            get
            {
                return GetType()
                    .GetFields()
                    .Where(p => p.FieldType == typeof(bool) && (bool)p.GetValue(this))
                    .Select(p => p.Name);
            }
        }

        internal void DoSettingsWindowContents(Rect inRect)
        {
            checked
            {
                Listing_Standard l = new Listing_Standard();
                l.Begin(inRect);

                l.CheckboxLabeled("MOT_genericPatches_title".Translate(), ref genericPatches, "MOT_genericPatches_desc".Translate());

                l.GapLine(24);

                l.CheckboxLabeled("MOT_hygieneIrrigation_title".Translate(), ref hygieneIrrigation, "MOT_hygieneIrrigation_desc".Translate());
                l.CheckboxLabeled("MOT_hygieneHeating_title".Translate(), ref hygieneHeating, "MOT_hygieneHeating_desc".Translate());

                l.GapLine(24);

                l.CheckboxLabeled("MOT_vfeClassicalIntegrations_title".Translate(), ref vfeClassicalIntegrations, "MOT_vfeClassicalIntegrations_desc".Translate());

                l.GapLine(24);

                l.Label("MOT_rimFantasyProduct_title".Translate() + ": <color=#FFFF44>" + rimFantasyProduct.ToString() + "</color>", tooltip: "MOT_rimFantasyProduct_desc".Translate());
                rimFantasyProduct = Mathf.RoundToInt(l.Slider(rimFantasyProduct, 1, 400));

                l.Label("MOT_rimFantasyWork_title".Translate() + ": <color=#FFFF44>" + rimFantasyWork.ToString() + "</color>", tooltip: "MOT_rimFantasyWork_desc".Translate());
                rimFantasyWork = Mathf.RoundToInt(l.Slider(rimFantasyWork, 300, 3000));

                l.GapLine(24);

                Rect rectDefaultSettings = l.GetRect(30f);
                TooltipHandler.TipRegion(rectDefaultSettings, "MOT_defaultSettings_tooltip".Translate());
                if (Widgets.ButtonText(rectDefaultSettings, "MOT_defaultSettings".Translate(), true, true, true))
                {
                    genericPatches = false;
                    hygieneHeating = false;
                    hygieneIrrigation = true;
                    vfeClassicalIntegrations = true;
                    rimFantasyProduct = 10;
                    rimFantasyWork = 800;
                }

                l.GapLine(24);
                Rect rectApplyRestart = l.GetRect(30f);
                TooltipHandler.TipRegion(rectApplyRestart, "MOT_applyRestart_tooltip".Translate());
                if (Widgets.ButtonText(rectApplyRestart, "MOT_applyRestart_button".Translate(), true, true, true))
                {
                    MOTweaksMod.Instance.WriteSettings();
                    GenCommandLine.Restart();
                }

                l.End();
                base.Write();
            }
        }
    }
}
