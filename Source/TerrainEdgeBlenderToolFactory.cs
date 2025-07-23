using LudeonTK;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainEdgeBlenderToolFactory
    {
        public static void CreateEdgeBlenderTool(float blenderRadius, float blendStrength)
        {
            // Store current tool settings for dragging
            TerrainBrushState.CurrentBrushRadius = blenderRadius;
            
            // Create the edge blender tool with blend-while-dragging functionality
            string toolLabel = $"Edge Blender (radius {blenderRadius}, {(blendStrength * 100):F0}% strength)";
            
            DebugTools.curTool = new DebugTool(toolLabel, delegate
            {
                // On click, start blending and blend the initial cell
                IntVec3 clickedCell = UI.MouseCell();
                TerrainBrushState.IsPainting = true;
                TerrainBrushState.LastPaintedCell = clickedCell;
                TerrainEdgeBlender.BlendTerrainEdges(clickedCell, blenderRadius, blendStrength);
            }, delegate
            {
                // On mouse over (called every frame while tool is active)
                IntVec3 currentCell = UI.MouseCell();
                
                // Handle mouse release - stop blending
                if (TerrainBrushState.IsPainting && !UnityEngine.Input.GetMouseButton(0))
                {
                    TerrainBrushState.IsPainting = false;
                    TerrainBrushState.LastPaintedCell = IntVec3.Invalid;
                }
                
                // Handle blending while dragging
                if (TerrainBrushState.IsPainting && UnityEngine.Input.GetMouseButton(0) && currentCell != TerrainBrushState.LastPaintedCell)
                {
                    TerrainEdgeBlender.BlendTerrainEdges(currentCell, blenderRadius, blendStrength);
                    TerrainBrushState.LastPaintedCell = currentCell;
                }
                
                // Always render blender preview (reuse the spray renderer with lighter preview)
                TerrainSprayRenderer.RenderSprayPreview(currentCell, blenderRadius);
            });
        }
    }
}
