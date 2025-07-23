using LudeonTK;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainSprayRenderer
    {
        public static void RenderSprayPreview(IntVec3 center, float radius)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            // For single cell (radius 0), just highlight the center cell
            if (radius <= 0f)
            {
                Vector3 centerWorld = center.ToVector3Shifted();
                Vector2 centerScreen = centerWorld.MapToUIPosition();
                DevGUI.DrawBox(new Rect(centerScreen.x - 12f, centerScreen.y - 12f, 24f, 24f), 2);
                return;
            }

            // For circular sprays, draw individual cell previews with lighter color to indicate sparse coverage
            int searchRadius = Mathf.CeilToInt(radius);
            for (int x = -searchRadius; x <= searchRadius; x++)
            {
                for (int z = -searchRadius; z <= searchRadius; z++)
                {
                    IntVec3 cell = center + new IntVec3(x, 0, z);
                    
                    // Check if the cell is within the circular radius
                    float distance = Mathf.Sqrt(x * x + z * z);
                    if (distance <= radius && cell.InBounds(map))
                    {
                        // Draw a box for each affected cell using screen coordinates
                        Vector3 cellWorld = cell.ToVector3Shifted();
                        Vector2 cellScreen = cellWorld.MapToUIPosition();
                        
                        // Draw a thinner box to indicate spray coverage (lighter than brush)
                        DevGUI.DrawBox(new Rect(cellScreen.x - 12f, cellScreen.y - 12f, 24f, 24f), 1);
                    }
                }
            }
        }
    }
}
