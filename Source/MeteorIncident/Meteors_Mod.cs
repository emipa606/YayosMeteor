using Mlie;
using UnityEngine;
using Verse;

namespace MeteorIncident;

public class Meteors_Mod : Mod
{
    public static string currentVersion;

    private readonly Meteor_Settings settings;

    public Meteors_Mod(ModContentPack modContentPack) : base(modContentPack)
    {
        settings = GetSettings<Meteor_Settings>();
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(modContentPack.ModMetaData);
    }

    public override string SettingsCategory()
    {
        return "YAYO Meteor";
    }

    public override void DoSettingsWindowContents(Rect rect)
    {
        Meteor_Settings.DoWindowContents(rect);
        settings.Write();
    }
}