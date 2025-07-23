using LudeonTK;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainSprayToolFactory
    {
        public static void CreateSprayToolForTerrain(TerrainDef terrain, float sprayRadius, float coverage)
        {
            // Store current tool settings for dragging
            TerrainBrushState.CurrentTerrain = terrain;
            TerrainBrushState.CurrentBrushRadius = sprayRadius;
            TerrainBrushState.CurrentSprayCoverage = coverage;
            
            // Create the spray tool with spray-while-dragging functionality
            string toolLabel = $"{terrain.LabelCap} spray (radius {sprayRadius}, {(coverage * 100):F0}% coverage)";
            
            DebugTools.curTool = new DebugTool(toolLabel, delegate
            {
                // On click, start spraying and spray the initial cell
                IntVec3 clickedCell = UI.MouseCell();
                TerrainBrushState.IsSpraying = true;
                TerrainBrushState.LastSprayedCell = clickedCell;
                TerrainSprayLogic.ApplyTerrainSpray(clickedCell, terrain, sprayRadius, coverage);
            }, delegate
            {
                // On mouse over (called every frame while tool is active)
                IntVec3 currentCell = UI.MouseCell();
                
                // Handle mouse release - stop spraying
                if (TerrainBrushState.IsSpraying && !UnityEngine.Input.GetMouseButton(0))
                {
                    TerrainBrushState.IsSpraying = false;
                    TerrainBrushState.LastSprayedCell = IntVec3.Invalid;
                }
                
                // Handle spraying while dragging
                if (TerrainBrushState.IsSpraying && UnityEngine.Input.GetMouseButton(0) && currentCell != TerrainBrushState.LastSprayedCell)
                {
                    TerrainSprayLogic.ApplyTerrainSpray(currentCell, terrain, sprayRadius, coverage);
                    TerrainBrushState.LastSprayedCell = currentCell;
                }
                
                // Always render spray preview
                TerrainSprayRenderer.RenderSprayPreview(currentCell, sprayRadius);
            });
        }
    }
}
