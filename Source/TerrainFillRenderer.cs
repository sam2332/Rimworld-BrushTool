using System.Collections.Generic;
using LudeonTK;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainFillRenderer
    {
        private const int MAX_PREVIEW_CELLS = 500;
        
        public static void RenderFillPreview(IntVec3 position, TerrainDef targetTerrain)
        {
            Map map = Find.CurrentMap;
            if (map == null || !position.InBounds(map)) return;

            // Don't preview if hovering over same terrain type
            TerrainDef currentTerrain = map.terrainGrid.TerrainAt(position);
            if (currentTerrain == targetTerrain) return;

            // Get preview cells with limit
            List<IntVec3> previewCells = TerrainFillLogic.GetFillPreviewCells(position, map, MAX_PREVIEW_CELLS);
            
            if (previewCells.Count == 0) return;

            // If the area is too large, show warning instead of preview
            if (previewCells.Count >= MAX_PREVIEW_CELLS)
            {
                // Just highlight the start cell and show a message
                Vector3 posWorld = position.ToVector3Shifted();
                Vector2 posScreen = posWorld.MapToUIPosition();
                DevGUI.DrawBox(new Rect(posScreen.x - 15f, posScreen.y - 15f, 30f, 30f), 3);
                
                // Show warning message (this could be improved with a proper UI element)
                return;
            }

            // Draw preview for reasonable-sized areas
            foreach (IntVec3 cell in previewCells)
            {
                if (cell.InBounds(map))
                {
                    Vector3 cellWorld = cell.ToVector3Shifted();
                    Vector2 cellScreen = cellWorld.MapToUIPosition();
                    
                    // Draw a lighter box for fill preview
                    DevGUI.DrawBox(new Rect(cellScreen.x - 12f, cellScreen.y - 12f, 24f, 24f), 1);
                }
            }

            // Highlight the clicked cell with a thicker border
            Vector3 clickWorld = position.ToVector3Shifted();
            Vector2 clickScreen = clickWorld.MapToUIPosition();
            DevGUI.DrawBox(new Rect(clickScreen.x - 15f, clickScreen.y - 15f, 30f, 30f), 3);
        }
    }
}
