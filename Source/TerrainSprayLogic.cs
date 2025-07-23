using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainSprayLogic
    {
        private static System.Random random = new System.Random();
        
        public static void ApplyTerrainSpray(IntVec3 center, TerrainDef terrain, float radius, float coverage = 0.1f)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            // Capture undo data before making changes
            TerrainUndoSystem.CaptureBeforeAction(center, radius);

            // Calculate the search area bounds
            int searchRadius = Mathf.CeilToInt(radius);
            
            // Paint terrain in a circular pattern centered on the clicked cell with random coverage
            for (int x = -searchRadius; x <= searchRadius; x++)
            {
                for (int z = -searchRadius; z <= searchRadius; z++)
                {
                    IntVec3 cell = center + new IntVec3(x, 0, z);
                    
                    // Check if the cell is within the circular radius
                    float distance = Mathf.Sqrt(x * x + z * z);
                    if (distance <= radius && cell.InBounds(map))
                    {
                        // Apply random coverage - only paint if random value is within coverage percentage
                        if (random.NextDouble() < coverage)
                        {
                            map.terrainGrid.SetTerrain(cell, terrain);
                        }
                    }
                }
            }
        }
    }
}
