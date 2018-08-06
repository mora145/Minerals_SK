﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;   // Always needed
using RimWorld;      // RimWorld specific functions 
using Verse;         // RimWorld universal objects 

namespace Minerals
{
    /// <summary>
    /// SaltCrystal class
    /// </summary>
    /// <author>zachary-foster</author>
    /// <permission>No restrictions</permission>
    public class SaltCrystal : DynamicMineral
    {

        public override float GrowthRate
        {
            get
            {
                return base.GrowthRate * ThingDef_SaltCrystal.GrowthRateBonus(Position, Map);
            }
        }


        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder(base.GetInspectString());
            if (Map.terrainGrid.TerrainAt(Position).defName == "WaterOceanShallow") // melts in water
            {
                stringBuilder.AppendLine("Dissolving in water.");
            }
            return stringBuilder.ToString().TrimEndNewlines();
        }

     }       
        

    /// <summary>
    /// ThingDef_StaticMineral class.
    /// </summary>
    /// <author>zachary-foster</author>
    /// <permission>No restrictions</permission>
    public class ThingDef_SaltCrystal : ThingDef_DynamicMineral
    {
        public static float GrowthRateBonus(IntVec3 aPosition, Map aMap)
        {
            float bonus = 1f;
            TerrainDef terrain = aMap.terrainGrid.TerrainAt(aPosition);
    
            if (terrain.defName == "TKKN_SandBeachWetSalt") // Grows faster on wet sand
            {
                bonus = bonus * 3;
            } else if (terrain.defName == "WaterOceanShallow") // melts in water
            {
                bonus = Math.Abs(bonus) * -1;
            }
    
            return bonus;
        }
    
        public override float GrowthRateAtPos(Map aMap, IntVec3 aPosition) 
        {
            return base.GrowthRateAtPos(aMap, aPosition) * ThingDef_SaltCrystal.GrowthRateBonus(aPosition, aMap);
        }
    }
    
}