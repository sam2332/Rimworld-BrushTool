using LudeonTK;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainRectToolFactory
    {
        public static void CreateRectToolForTerrain(TerrainDef terrain, bool filled)
        {
            // Create the rectangle tool with two-click functionality
            string toolLabel = filled ? 
                $"{terrain.LabelCap} filled rectangle tool" : 
                $"{terrain.LabelCap} border rectangle tool";
            
            IntVec3 startPoint = IntVec3.Invalid;
            bool hasStartPoint = false;
            
            DebugTools.curTool = new DebugTool(toolLabel, delegate
            {
                // On click
                IntVec3 clickedCell = UI.MouseCell();
                
                if (!hasStartPoint)
                {
                    // First click: set start point (corner)
                    startPoint = clickedCell;
                    hasStartPoint = true;
                    string rectType = filled ? "filled rectangle" : "border rectangle";
                    Log.Message($"Rectangle start corner set at {startPoint}. Click again to set opposite corner and draw {rectType}.");
                }
                else
                {
                    // Second click: draw rectangle from start to end
                    IntVec3 endPoint = clickedCell;
                    TerrainRectLogic.ApplyTerrainRect(startPoint, endPoint, terrain, filled);
                    string rectType = filled ? "filled rectangle" : "border rectangle";
                    Log.Message($"{rectType} drawn from {startPoint} to {endPoint}");
                    
                    // Reset for next rectangle
                    hasStartPoint = false;
                    startPoint = IntVec3.Invalid;
                }
            }, delegate
            {
                // On mouse over (called every frame while tool is active)
                IntVec3 currentCell = UI.MouseCell();
                
                if (hasStartPoint)
                {
                    // Preview the rectangle from start to current mouse position
                    TerrainRectRenderer.RenderRectPreview(startPoint, currentCell, filled);
                }
                else
                {
                    // Show start point preview
                    TerrainRectRenderer.RenderStartPointPreview(currentCell);
                }
            });
        }
    }
}
