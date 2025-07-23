using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainFillLogic
    {
        public static void ApplyTerrainFill(IntVec3 startCell, TerrainDef newTerrain)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            TerrainDef originalTerrain = map.terrainGrid.TerrainAt(startCell);
            
            // Don't fill if clicking on the same terrain type
            if (originalTerrain == newTerrain) return;

            // Get all cells to fill
            List<IntVec3> cellsToFill = GetFillCells(startCell, originalTerrain, map);

            // Apply the terrain to all cells
            foreach (IntVec3 cell in cellsToFill)
            {
                map.terrainGrid.SetTerrain(cell, newTerrain);
            }

            Log.Message($"Filled {cellsToFill.Count} cells with {newTerrain.LabelCap}");
        }

        public static List<IntVec3> GetFillCells(IntVec3 startCell, TerrainDef targetTerrain, Map map, int maxCells = 10000)
        {
            List<IntVec3> result = new List<IntVec3>();
            HashSet<IntVec3> visited = new HashSet<IntVec3>();
            Queue<IntVec3> toProcess = new Queue<IntVec3>();

            if (!startCell.InBounds(map) || map.terrainGrid.TerrainAt(startCell) != targetTerrain)
            {
                return result;
            }

            toProcess.Enqueue(startCell);
            visited.Add(startCell);

            // 4-directional neighbors (no diagonals for cleaner fills)
            IntVec3[] directions = {
                new IntVec3(0, 0, 1),   // North
                new IntVec3(1, 0, 0),   // East
                new IntVec3(0, 0, -1),  // South
                new IntVec3(-1, 0, 0)   // West
            };

            while (toProcess.Count > 0 && result.Count < maxCells)
            {
                IntVec3 current = toProcess.Dequeue();
                result.Add(current);

                // Check all 4 neighbors
                foreach (IntVec3 direction in directions)
                {
                    IntVec3 neighbor = current + direction;
                    
                    if (neighbor.InBounds(map) && 
                        !visited.Contains(neighbor) && 
                        map.terrainGrid.TerrainAt(neighbor) == targetTerrain)
                    {
                        visited.Add(neighbor);
                        toProcess.Enqueue(neighbor);
                    }
                }
            }

            return result;
        }

        public static List<IntVec3> GetFillPreviewCells(IntVec3 startCell, Map map, int maxPreviewCells = 500)
        {
            if (!startCell.InBounds(map)) return new List<IntVec3>();
            
            TerrainDef targetTerrain = map.terrainGrid.TerrainAt(startCell);
            return GetFillCells(startCell, targetTerrain, map, maxPreviewCells);
        }
    }
}
