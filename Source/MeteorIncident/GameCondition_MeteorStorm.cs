using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace MeteorIncident;

public class GameCondition_MeteorStorm : GameCondition
{
    private const int minTicks = 300;

    private static readonly IntRange ticksBetweenStrikes = new(minTicks, minTicks);
    public static int NextMeteorTicks = 9999;

    private readonly Meteor_Object meteor = new();

    private readonly SkyColorSet meteorStormColors = new(new ColorInt(251, 105, 45).ToColor,
        new Color(255f, 242f, 200f), new Color(0.8f, 0.5f, 0.5f), 0.85f);

    private bool foundCell;

    private int strikeCount = 1;
    private IntVec3 targetPoint = IntVec3.Zero;
    private IntVec3 targetPoint2 = IntVec3.Zero;

    public static int NextMeteorTicksReset
    {
        get
        {
            var diff = Mathf.Clamp(Find.Storyteller.difficulty.threatScale, 0.3f, 10f);
            return Mathf.RoundToInt(Meteor_Settings.MeteorDelay * 60 / diff);
        }
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref NextMeteorTicks, "nextMeteorTicks", NextMeteorTicksReset);
    }

    public override void GameConditionTick()
    {
        if (!(Find.TickManager.TicksGame <= NextMeteorTicks))
        {
            if (!foundCell)
            {
                foundCell = tryFindCell(out targetPoint, SingleMap);
                strikeCount = Rand.Range(3, 5);
            }

            if (foundCell && strikeCount > 0)
            {
                strikeCount--;
                if (strikeCount == 0)
                {
                    Meteor_Object.GenerateMeteor(targetPoint, SingleMap, false);
                }
                else
                {
                    Meteor_Object.GenerateMeteor(targetPoint, SingleMap);
                }

                NextMeteorTicks = Find.TickManager.TicksGame + ticksBetweenStrikes.RandomInRange;
            }
        }


        if (!foundCell || Find.TickManager.TicksGame % 40 != 0 || strikeCount <= 0)
        {
            return;
        }

        var foundSkyfallerCell = CellFinderLoose.TryFindSkyfallerCell(ThingDefOf.ShipChunkIncoming, SingleMap,
            TerrainAffordanceDefOf.Walkable,
            out targetPoint2, 10, targetPoint, 20, false, true, true, true, false);
        if (foundSkyfallerCell)
        {
            Meteor_Object.GenerateMeteor2(targetPoint2, SingleMap);
        }
    }

    public override float SkyTargetLerpFactor(Map map)
    {
        return GameConditionUtility.LerpInOutValue(this, 5000f, 0.5f);
    }

    public override SkyTarget? SkyTarget(Map map)
    {
        return new SkyTarget(0.85f, meteorStormColors, 1f, 1f);
    }


    private static bool tryFindCell(out IntVec3 cell, Map map)
    {
        bool foundSkyfallerCell;

        if (Rand.Chance(0.35f))
        {
            // colonist target

            var arPawn = new List<Pawn>();
            var arPawnAll = map.mapPawns.FreeColonists.ToList();

            if (Rand.Chance(0.7f))
            {
                // out door target
                foreach (var p in arPawnAll)
                {
                    var tmpRoom = p.GetRoom(RegionType.Set_Passable);
                    if (tmpRoom is not { PsychologicallyOutdoors: false })
                    {
                        // outdoor pawn
                        arPawn.Add(p);
                    }
                }

                if (arPawn.Count == 0)
                {
                    foundSkyfallerCell = CellFinderLoose.TryFindSkyfallerCell(ThingDefOf.MeteoriteIncoming, map,
                        TerrainAffordanceDefOf.Walkable,
                        out cell, 10,
                        default, -1, false, true, true, true, false);
                    return foundSkyfallerCell;
                }
            }
            else
            {
                // any target
                arPawn = arPawnAll;
            }


            var tpoint = arPawn[Rand.Range(0, arPawn.Count)].Position;

            foundSkyfallerCell = CellFinderLoose.TryFindSkyfallerCell(ThingDefOf.MeteoriteIncoming, map,
                TerrainAffordanceDefOf.Walkable, out cell, 10,
                tpoint, 30,
                false, true, true, true, false);
        }
        else
        {
            // random area target
            foundSkyfallerCell = CellFinderLoose.TryFindSkyfallerCell(ThingDefOf.MeteoriteIncoming, map,
                TerrainAffordanceDefOf.Walkable, out cell, 10,
                default, -1,
                false, true, true, true, false);
        }

        return foundSkyfallerCell;
    }
}