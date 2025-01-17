﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;   // Always needed
using RimWorld;      // RimWorld specific functions 
using Verse;         // RimWorld universal objects 

namespace Minerals
{
    public static class mapBuilder
    {

        public static void initAll(Map map)
        {
            if (MineralsMain.Settings.removeStartingChunksSetting)
            {
                removeStartingChunks(map);
            }
            initStaticMinerals(map);
            Log.Message("Minerals loaded");
        }

        public static void initStaticMinerals(Map map)
        {
            List<string> spawned =  new List<string>();

            // Spawn static minerals
            foreach (ThingDef_StaticMineral mineralType in DefDatabase<ThingDef_StaticMineral>.AllDefs)
            {
                if (mineralType.GetType() == typeof(ThingDef_StaticMineral) && (! spawned.Contains(mineralType.defName)))
                {
                    mineralType.InitNewMap(map);
                    spawned.Add(mineralType.defName);
                }
            }

            // spawn dynamic minerals
            foreach (ThingDef_StaticMineral mineralType in DefDatabase<ThingDef_StaticMineral>.AllDefs)
            {
                if (mineralType.GetType() == typeof(ThingDef_DynamicMineral) && (! spawned.Contains(mineralType.defName)))
                {
                    mineralType.InitNewMap(map);
                    spawned.Add(mineralType.defName);
                }
            }

            // spawn large minerals
            foreach (ThingDef_StaticMineral mineralType in DefDatabase<ThingDef_StaticMineral>.AllDefs)
            {
                if (mineralType.GetType() == typeof(ThingDef_BigMineral) && (! spawned.Contains(mineralType.defName)))
                {
                    mineralType.InitNewMap(map);
                    spawned.Add(mineralType.defName);
                }
            }

            // spawn everything else
            foreach (ThingDef_StaticMineral mineralType in DefDatabase<ThingDef_StaticMineral>.AllDefs)
            {
                if (! spawned.Contains(mineralType.defName))
                {
                    mineralType.InitNewMap(map);
                }
            }
        }

        public static void removeStartingChunks(Map map)
        {
            string[] toRemove = {"ChunkSandstone", "ChunkGranite", "ChunkLimestone", "ChunkSlate", "ChunkMarble", "ChunkLava", "ChunkClaystone", "Filth_RubbleRock"};
            List<Thing> thingsToCheck = map.listerThings.AllThings;
            for (int i = thingsToCheck.Count - 1; i >= 0; i--)
            {
                if (toRemove.Any(thingsToCheck[i].def.defName.Equals))
                {
                    thingsToCheck[i].Destroy(DestroyMode.Vanish);
                }
            }
        }

    }
}
