using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainRectLogic
    {
        public static void ApplyTerrainRect(IntVec3 corner1, IntVec3 corner2, TerrainDef terrain, bool filled)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            // Get all cells within the rectangle
            List<IntVec3> rectCells = filled ? 
                GetFilledRectangleCells(corner1, corner2) : 
                GetBorderRectangleCells(corner1, corner2);

            // Capture undo data before making changes
            TerrainUndoSystem.CaptureBeforeAction(rectCells);

            // Paint terrain on all cells in the rectangle
            foreach (IntVec3 cell in rectCells)
            {
                if (cell.InBounds(map))
                {
                    map.terrainGrid.SetTerrain(cell, terrain);
                }
            }
        }

        private static List<IntVec3> GetFilledRectangleCells(IntVec3 corner1, IntVec3 corner2)
        {
            List<IntVec3> cells = new List<IntVec3>();
            
            // Calculate the bounds of the rectangle
            int minX = Mathf.Min(corner1.x, corner2.x);
            int maxX = Mathf.Max(corner1.x, corner2.x);
            int minZ = Mathf.Min(corner1.z, corner2.z);
            int maxZ = Mathf.Max(corner1.z, corner2.z);

            // Fill all cells within the rectangle bounds
            for (int x = minX; x <= maxX; x++)
            {
                for (int z = minZ; z <= maxZ; z++)
                {
                    cells.Add(new IntVec3(x, 0, z));
                }
            }

            return cells;
        }

        private static List<IntVec3> GetBorderRectangleCells(IntVec3 corner1, IntVec3 corner2)
        {
            List<IntVec3> cells = new List<IntVec3>();
            
            // Calculate the bounds of the rectangle
            int minX = Mathf.Min(corner1.x, corner2.x);
            int maxX = Mathf.Max(corner1.x, corner2.x);
            int minZ = Mathf.Min(corner1.z, corner2.z);
            int maxZ = Mathf.Max(corner1.z, corner2.z);

            // Only add border cells (perimeter)
            for (int x = minX; x <= maxX; x++)
            {
                for (int z = minZ; z <= maxZ; z++)
                {
                    // Only include cells that are on the border
                    if (x == minX || x == maxX || z == minZ || z == maxZ)
                    {
                        cells.Add(new IntVec3(x, 0, z));
                    }
                }
            }

            return cells;
        }
    }
}
