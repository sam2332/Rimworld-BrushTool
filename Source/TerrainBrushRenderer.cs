using LudeonTK;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainBrushRenderer
    {
        public static void RenderBrushPreview(IntVec3 center, float radius)
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

            // For circular brushes, draw individual cell previews
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
                        
                        // Draw a small box at each cell position
                        DevGUI.DrawBox(new Rect(cellScreen.x - 12f, cellScreen.y - 12f, 24f, 24f), 1);
                    }
                }
            }
        }
    }
}
