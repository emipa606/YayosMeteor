using UnityEngine;
using Verse;

namespace MeteorIncident;

public class Meteor_Settings : ModSettings
{
    public static int MeteorDelay = 8;

    public void DoWindowContents(Rect rect)
    {
        var ls = new Listing_Standard();
        ls.Begin(rect);
        ls.Gap(10f);
        ls.MeteorIntervalSlider("Meteo_interval".Translate(), ref MeteorDelay, "Meteo_interval_tt".Translate());
        ls.Gap(10f);
        if (Meteors_Mod.currentVersion != null)
        {
            ls.Gap();
            GUI.contentColor = Color.gray;
            ls.Label("Meteo_modVersion".Translate(Meteors_Mod.currentVersion));
            GUI.contentColor = Color.white;
        }

        ls.End();
    }

    public override void ExposeData()
    {
        Scribe_Values.Look(ref MeteorDelay, "MeteorDelay", 8);
    }
}