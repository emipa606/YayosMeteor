using Mlie;
using UnityEngine;
using Verse;

namespace MeteorIncident;

public class Meteors_Mod : Mod
{
    public static string currentVersion;

    public readonly Meteor_Settings settings;

    public Meteors_Mod(ModContentPack modContentPack) : base(modContentPack)
    {
        settings = GetSettings<Meteor_Settings>();
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(ModLister.GetActiveModWithIdentifier("Mlie.YayosMeteor"));
    }

    public override string SettingsCategory()
    {
        return "YAYO Meteor";
    }

    public override void DoSettingsWindowContents(Rect rect)
    {
        settings.DoWindowContents(rect);
        settings.Write();
    }
}