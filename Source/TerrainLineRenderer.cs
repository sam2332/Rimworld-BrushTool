using System.Collections.Generic;
using LudeonTK;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainLineRenderer
    {
        public static void RenderLinePreview(IntVec3 start, IntVec3 end)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            // Get all cells along the line
            List<IntVec3> lineCells = GetLineCells(start, end);

            // Draw a box for each cell in the line
            foreach (IntVec3 cell in lineCells)
            {
                if (cell.InBounds(map))
                {
                    Vector3 cellWorld = cell.ToVector3Shifted();
                    Vector2 cellScreen = cellWorld.MapToUIPosition();
                    
                    // Draw a box at each cell position with thicker border for line preview
                    DevGUI.DrawBox(new Rect(cellScreen.x - 12f, cellScreen.y - 12f, 24f, 24f), 2);
                }
            }

            // Highlight start point with different color/style
            Vector3 startWorld = start.ToVector3Shifted();
            Vector2 startScreen = startWorld.MapToUIPosition();
            DevGUI.DrawBox(new Rect(startScreen.x - 15f, startScreen.y - 15f, 30f, 30f), 3);
        }

        public static void RenderStartPointPreview(IntVec3 position)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            if (position.InBounds(map))
            {
                Vector3 posWorld = position.ToVector3Shifted();
                Vector2 posScreen = posWorld.MapToUIPosition();
                
                // Draw a highlighted box for the potential start point
                DevGUI.DrawBox(new Rect(posScreen.x - 12f, posScreen.y - 12f, 24f, 24f), 2);
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
