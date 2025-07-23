using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainLineLogic
    {
        public static void ApplyTerrainLine(IntVec3 start, IntVec3 end, TerrainDef terrain)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            // Get all cells along the line using Bresenham's line algorithm
            List<IntVec3> lineCells = GetLineCells(start, end);

            // Capture undo data before making changes
            TerrainUndoSystem.CaptureBeforeAction(lineCells);

            // Paint terrain on all cells in the line
            foreach (IntVec3 cell in lineCells)
            {
                if (cell.InBounds(map))
                {
                    map.terrainGrid.SetTerrain(cell, terrain);
                }
            }
        }

        private static List<IntVec3> GetLineCells(IntVec3 start, IntVec3 end)
        {
            List<IntVec3> cells = new List<IntVec3>();
            
            int x0 = start.x;
            int z0 = start.z;
            int x1 = end.x;
            int z1 = end.z;

            int dx = Mathf.Abs(x1 - x0);
            int dz = Mathf.Abs(z1 - z0);
            int x = x0;
            int z = z0;
            int n = 1 + dx + dz;
            int x_inc = (x1 > x0) ? 1 : -1;
            int z_inc = (z1 > z0) ? 1 : -1;
            int error = dx - dz;

            dx *= 2;
            dz *= 2;

            for (; n > 0; --n)
            {
                cells.Add(new IntVec3(x, 0, z));

                if (error > 0)
                {
                    x += x_inc;
                    error -= dz;
                }
                else
                {
                    z += z_inc;
                    error += dx;
                }
            }

            return cells;
        }
    }
}
