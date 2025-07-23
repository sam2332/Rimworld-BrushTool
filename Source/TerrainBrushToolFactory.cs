using LudeonTK;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainBrushToolFactory
    {
        public static void CreateBrushToolForTerrain(TerrainDef terrain, float brushRadius)
        {
            // Store current tool settings for dragging
            TerrainBrushState.CurrentTerrain = terrain;
            TerrainBrushState.CurrentBrushRadius = brushRadius;
            
            // Create the brush tool with paint-while-dragging functionality
            string toolLabel = $"{terrain.LabelCap} brush (radius {brushRadius})";
            
            DebugTools.curTool = new DebugTool(toolLabel, delegate
            {
                // On click, start painting and paint the initial cell
                IntVec3 clickedCell = UI.MouseCell();
                TerrainBrushState.IsPainting = true;
                TerrainBrushState.LastPaintedCell = clickedCell;
                TerrainBrushLogic.ApplyTerrainBrush(clickedCell, terrain, brushRadius);
            }, delegate
            {
                // On mouse over (called every frame while tool is active)
                IntVec3 currentCell = UI.MouseCell();
                
                // Handle mouse release - stop painting
                if (TerrainBrushState.IsPainting && !UnityEngine.Input.GetMouseButton(0))
                {
                    TerrainBrushState.IsPainting = false;
                    TerrainBrushState.LastPaintedCell = IntVec3.Invalid;
                }
                
                // Handle painting while dragging
                if (TerrainBrushState.IsPainting && UnityEngine.Input.GetMouseButton(0) && currentCell != TerrainBrushState.LastPaintedCell)
                {
                    TerrainBrushLogic.ApplyTerrainBrush(currentCell, terrain, brushRadius);
                    TerrainBrushState.LastPaintedCell = currentCell;
                }
                
                // Always render brush preview
                TerrainBrushRenderer.RenderBrushPreview(currentCell, brushRadius);
            });
        }
    }
}
