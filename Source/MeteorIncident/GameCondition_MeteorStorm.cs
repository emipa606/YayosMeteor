using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace MeteorIncident;

public class GameCondition_MeteorStorm : GameCondition
{
    private static readonly int minTicks = 300;

    private static readonly IntRange TicksBetweenStrikes = new IntRange(minTicks, minTicks);
    public static int nextMeteorTicks = 9999;

    private readonly Meteor_Object meteor = new Meteor_Object();

    private readonly SkyColorSet MeteorStormColors = new SkyColorSet(new ColorInt(251, 105, 45).ToColor,
        new Color(255f, 242f, 200f), new Color(0.8f, 0.5f, 0.5f), 0.85f);

    private bool foundCell;

    private int strikeCount = 1;
    private IntVec3 targetPoint = IntVec3.Zero;
    private IntVec3 targetPoint2 = IntVec3.Zero;

    public static int nextMeteorTicksReset
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
        Scribe_Values.Look(ref nextMeteorTicks, "nextMeteorTicks", nextMeteorTicksReset);
    }

    public override void GameConditionTick()
    {
        if (!(Find.TickManager.TicksGame <= nextMeteorTicks))
        {
            if (!foundCell)
            {
                foundCell = TryFindCell(out targetPoint, SingleMap);
                strikeCount = Rand.Range(3, 5);
            }

            if (foundCell && strikeCount > 0)
            {
                strikeCount--;
                if (strikeCount == 0)
                {
                    meteor.generateMeteor(targetPoint, SingleMap, false);
                }
                else
                {
                    meteor.generateMeteor(targetPoint, SingleMap);
                }

                nextMeteorTicks = Find.TickManager.TicksGame + TicksBetweenStrikes.RandomInRange;
            }
        }


        if (!foundCell || Find.TickManager.TicksGame % 40 != 0 || strikeCount <= 0)
        {
            return;
        }

        var foundSkyfallerCell = CellFinderLoose.TryFindSkyfallerCell(ThingDefOf.ShipChunkIncoming, SingleMap,
            out targetPoint2, 10, targetPoint, 20, false, true, true, true, false);
        if (foundSkyfallerCell)
        {
            meteor.generateMeteor2(targetPoint2, SingleMap);
        }
    }

    public override float SkyTargetLerpFactor(Map map)
    {
        return GameConditionUtility.LerpInOutValue(this, 5000f, 0.5f);
    }

    public override SkyTarget? SkyTarget(Map map)
    {
        return new SkyTarget(0.85f, MeteorStormColors, 1f, 1f);
    }


    private bool TryFindCell(out IntVec3 cell, Map map)
    {
        bool foundSkyfallerCell;

        if (Rand.Chance(0.35f))
        {
            // colonist target

            var ar_pawn = new List<Pawn>();
            var ar_pawn_all = map.mapPawns.FreeColonists.ToList();

            if (Rand.Chance(0.7f))
            {
                // out door target
                foreach (var p in ar_pawn_all)
                {
                    var tmp_room = p.GetRoom(RegionType.Set_Passable);
                    if (tmp_room is not { PsychologicallyOutdoors: false })
                    {
                        // outdoor pawn
                        ar_pawn.Add(p);
                    }
                }

                if (ar_pawn.Count == 0)
                {
                    foundSkyfallerCell = CellFinderLoose.TryFindSkyfallerCell(ThingDefOf.MeteoriteIncoming, map,
                        out cell, 10,
                        default, -1, false, true, true, true, false);
                    return foundSkyfallerCell;
                }
            }
            else
            {
                // any target
                ar_pawn = ar_pawn_all;
            }


            var tpoint = ar_pawn[Rand.Range(0, ar_pawn.Count)].Position;

            foundSkyfallerCell = CellFinderLoose.TryFindSkyfallerCell(ThingDefOf.MeteoriteIncoming, map, out cell, 10,
                tpoint, 30,
                false, true, true, true, false);
        }
        else
        {
            // random area target
            foundSkyfallerCell = CellFinderLoose.TryFindSkyfallerCell(ThingDefOf.MeteoriteIncoming, map, out cell, 10,
                default, -1,
                false, true, true, true, false);
        }

        return foundSkyfallerCell;
    }
}