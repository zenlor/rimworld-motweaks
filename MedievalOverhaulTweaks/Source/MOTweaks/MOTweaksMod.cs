using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MOTweaks
{
    public class MOTweaksMod: Mod
    {
        public const string ModName = "MOTweaks";
        public static MOTweaksSettings settings;
        public static MOTweaksMod Instance;

        public MOTweaksMod(ModContentPack pack) : base(pack)
        {
            settings = GetSettings<MOTweaksSettings>();
            Instance = this;
        }

        public override string SettingsCategory()
        {
            return ModName;
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            settings.DoSettingsWindowContents(inRect);
        }

        public override void WriteSettings()
        {
            base.WriteSettings();
            DefsAlterer.DoDefsAlter();
        }
    }

    [StaticConstructorOnStartup]
    public static class DefsAlterer
    {
        static DefsAlterer()
        {
            Setup();
            DoDefsAlter();
        }

        public static void Setup()
        { }

        public static void DoDefsAlter()
        { }

        public static void SetRimFantasySettings() {
            var defs = DefDatabase<RecipeDef>.AllDefs.Where(p => p.defName.StartsWith("MOT_MakeRFOre"));

            foreach (var def in defs)
            {
                def.workAmount = MOTweaksMod.settings.rimFantasyWork;
                def.products[0].count = MOTweaksMod.settings.rimFantasyProduct;
            }
        }
    }

}
