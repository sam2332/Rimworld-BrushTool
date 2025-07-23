using LudeonTK;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainLineToolFactory
    {
        public static void CreateLineToolForTerrain(TerrainDef terrain)
        {
            // Create the line tool with two-click functionality
            string toolLabel = $"{terrain.LabelCap} line tool";
            
            IntVec3 startPoint = IntVec3.Invalid;
            bool hasStartPoint = false;
            
            DebugTools.curTool = new DebugTool(toolLabel, delegate
            {
                // On click
                IntVec3 clickedCell = UI.MouseCell();
                
                if (!hasStartPoint)
                {
                    // First click: set start point
                    startPoint = clickedCell;
                    hasStartPoint = true;
                    Log.Message($"Line start point set at {startPoint}. Click again to set end point and draw line.");
                }
                else
                {
                    // Second click: draw line from start to end
                    IntVec3 endPoint = clickedCell;
                    TerrainLineLogic.ApplyTerrainLine(startPoint, endPoint, terrain);
                    Log.Message($"Line drawn from {startPoint} to {endPoint}");
                    
                    // Reset for next line
                    hasStartPoint = false;
                    startPoint = IntVec3.Invalid;
                }
            }, delegate
            {
                // On mouse over (called every frame while tool is active)
                IntVec3 currentCell = UI.MouseCell();
                
                if (hasStartPoint)
                {
                    // Preview the line from start to current mouse position
                    TerrainLineRenderer.RenderLinePreview(startPoint, currentCell);
                }
                else
                {
                    // Show start point preview
                    TerrainLineRenderer.RenderStartPointPreview(currentCell);
                }
            });
        }
    }
}
