using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MeteorIncident;

public class Meteor_Object
{
    public void generateMeteor(IntVec3 targetCell, Map targetMap, bool bl_big = true)
    {
        var thingList = new List<Thing>();

        if (bl_big)
        {
            thingList.Add(ThingMaker.MakeThing(ThingDefOf.ChunkSlagSteel));
        }
        else
        {
            switch (Rand.Range(0, 7))
            {
                default:
                    thingList.Add(ThingMaker.MakeThing(ThingDefOf.StandingLamp));
                    break;
                case 1:
                    thingList.Add(ThingMaker.MakeThing(ThingDefOf.SolarGenerator));
                    break;
                case 2:
                    thingList.Add(ThingMaker.MakeThing(ThingDefOf.WindTurbine));
                    break;
                case 3:
                    thingList.Add(ThingMaker.MakeThing(ThingDefOf.Heater));
                    break;
                case 4:
                    thingList.Add(ThingMaker.MakeThing(ThingDefOf.Cooler));
                    break;
                case 5:
                    thingList.Add(ThingMaker.MakeThing(ThingDefOf.Battery));
                    break;
            }
        }


        var smallMet = ThingDefOf.MeteoriteIncoming;
        var speed = 0.5f;
        smallMet.skyfaller.speed = speed;

        var dmg = 0.9f;
        float explosionSize;


        switch (Find.FactionManager.OfPlayer.def.techLevel)
        {
            default:
                explosionSize = 56f;
                break;
            case TechLevel.Animal:
                explosionSize = 36f;
                break;
            case TechLevel.Neolithic:
                explosionSize = 36f;
                break;
            case TechLevel.Medieval:
                explosionSize = 46f;
                break;
        }

        if (targetMap.TileInfo.hilliness.ToString() == "Impassable")
        {
            explosionSize *= 0.3f;
            if (Rand.Chance(0.4f))
            {
                thingList = [ThingMaker.MakeThing(ThingDefOf.StandingLamp)];
            }
        }


        if (!bl_big)
        {
            explosionSize = 2;
            dmg = 0f;
        }

        smallMet.skyfaller.explosionDamageFactor = dmg;
        smallMet.skyfaller.explosionRadius = explosionSize;
        smallMet.skyfaller.shrapnelDistanceFactor = explosionSize;
        smallMet.skyfaller.ticksToImpactRange = new IntRange(15, 125);
        SkyfallerMaker.SpawnSkyfaller(smallMet, thingList, targetCell, targetMap);
    }


    public void generateMeteor2(IntVec3 targetCell, Map targetMap)
    {
        var thingList = new List<Thing>
        {
            Rand.Chance(0.5f)
                ? ThingMaker.MakeThing(ThingDefOf.ChunkSlagSteel)
                : ThingMaker.MakeThing(ThingDefOf.MineableSteel)
        };

        var smallMet = ThingDefOf.ShipChunkIncoming;
        var speed = 0.5f;
        smallMet.skyfaller.speed = speed;

        smallMet.skyfaller.explosionDamageFactor = 0.01f;
        float explosionSize = 2;
        smallMet.skyfaller.explosionRadius = explosionSize;
        smallMet.skyfaller.shrapnelDistanceFactor = explosionSize;
        smallMet.skyfaller.ticksToImpactRange = new IntRange(15, 125);
        SkyfallerMaker.SpawnSkyfaller(smallMet, thingList, targetCell, targetMap);
    }
}