using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainBrushLogic
    {
        public static void ApplyTerrainBrush(IntVec3 center, TerrainDef terrain, float radius)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            // Calculate the search area bounds
            int searchRadius = Mathf.CeilToInt(radius);
            
            // Paint terrain in a circular pattern centered on the clicked cell
            for (int x = -searchRadius; x <= searchRadius; x++)
            {
                for (int z = -searchRadius; z <= searchRadius; z++)
                {
                    IntVec3 cell = center + new IntVec3(x, 0, z);
                    
                    // Check if the cell is within the circular radius
                    float distance = Mathf.Sqrt(x * x + z * z);
                    if (distance <= radius && cell.InBounds(map))
                    {
                        map.terrainGrid.SetTerrain(cell, terrain);
                    }
                }
            }
        }
    }
}
