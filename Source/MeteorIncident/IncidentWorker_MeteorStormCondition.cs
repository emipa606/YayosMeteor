using System;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace MeteorIncident;

public class IncidentWorker_MeteorStormCondition : IncidentWorker
{
    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        var target = (Map)parms.target;
        var duration = Mathf.RoundToInt(def.durationDays.RandomInRange * GenDate.TicksPerDay);
        GameCondition_MeteorStorm.NextMeteorTicks = Find.TickManager.TicksGame +
                                                    Mathf.Clamp(GameCondition_MeteorStorm.NextMeteorTicksReset, 1,
                                                        (int)Math.Round(duration * 0.9f));
        var conditionMeteor =
            (GameCondition_MeteorStorm)GameConditionMaker.MakeCondition(GameConditionDef.Named("MeteorStorm"),
                duration);
        target.gameConditionManager.RegisterCondition(conditionMeteor);
        var tmpLookTargets = new LookTargets();
        var arPawnAll = target.mapPawns.FreeColonists.ToList();
        foreach (var pawn in arPawnAll)
        {
            tmpLookTargets.targets.Add(new TargetInfo(pawn));
        }


        Find.LetterStack.ReceiveLetter("Meteo_letterTitle".Translate(),
            "Meteo_letterDesc".Translate(),
            LetterDefOf.ThreatBig,
            tmpLookTargets
        );
        return true;
    }
}