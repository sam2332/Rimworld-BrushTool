using LudeonTK;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainRectRenderer
    {
        public static void RenderStartPointPreview(IntVec3 position)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            if (position.InBounds(map))
            {
                Vector3 posWorld = position.ToVector3Shifted();
                Vector2 posScreen = posWorld.MapToUIPosition();
                
                // Draw a slightly larger highlight for the start point
                DevGUI.DrawBox(new Rect(posScreen.x - 15f, posScreen.y - 15f, 30f, 30f), 3);
            }
        }

        public static void RenderRectPreview(IntVec3 startCorner, IntVec3 endCorner, bool filled)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            // Calculate the bounds of the rectangle
            int minX = Mathf.Min(startCorner.x, endCorner.x);
            int maxX = Mathf.Max(startCorner.x, endCorner.x);
            int minZ = Mathf.Min(startCorner.z, endCorner.z);
            int maxZ = Mathf.Max(startCorner.z, endCorner.z);

            if (filled)
            {
                // Draw all cells in the filled rectangle with lighter preview
                for (int x = minX; x <= maxX; x++)
                {
                    for (int z = minZ; z <= maxZ; z++)
                    {
                        IntVec3 cell = new IntVec3(x, 0, z);
                        if (cell.InBounds(map))
                        {
                            Vector3 cellWorld = cell.ToVector3Shifted();
                            Vector2 cellScreen = cellWorld.MapToUIPosition();
                            
                            // Use lighter border for interior cells, heavier for border
                            int borderWeight = (x == minX || x == maxX || z == minZ || z == maxZ) ? 2 : 1;
                            DevGUI.DrawBox(new Rect(cellScreen.x - 12f, cellScreen.y - 12f, 24f, 24f), borderWeight);
                        }
                    }
                }
            }
            else
            {
                // Draw only border cells for border-only rectangle
                for (int x = minX; x <= maxX; x++)
                {
                    for (int z = minZ; z <= maxZ; z++)
                    {
                        // Only draw border cells (outline)
                        if (x == minX || x == maxX || z == minZ || z == maxZ)
                        {
                            IntVec3 cell = new IntVec3(x, 0, z);
                            if (cell.InBounds(map))
                            {
                                Vector3 cellWorld = cell.ToVector3Shifted();
                                Vector2 cellScreen = cellWorld.MapToUIPosition();
                                
                                DevGUI.DrawBox(new Rect(cellScreen.x - 12f, cellScreen.y - 12f, 24f, 24f), 2);
                            }
                        }
                    }
                }
            }

            // Highlight the start corner
            if (startCorner.InBounds(map))
            {
                Vector3 startWorld = startCorner.ToVector3Shifted();
                Vector2 startScreen = startWorld.MapToUIPosition();
                DevGUI.DrawBox(new Rect(startScreen.x - 15f, startScreen.y - 15f, 30f, 30f), 3);
            }

            // Highlight the current end corner
            if (endCorner.InBounds(map))
            {
                Vector3 endWorld = endCorner.ToVector3Shifted();
                Vector2 endScreen = endWorld.MapToUIPosition();
                DevGUI.DrawBox(new Rect(endScreen.x - 15f, endScreen.y - 15f, 30f, 30f), 3);
            }
        }
    }
}
