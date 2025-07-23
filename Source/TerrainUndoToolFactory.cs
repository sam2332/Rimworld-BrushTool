using LudeonTK;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainUndoToolFactory
    {
        public static void CreateUndoTool()
        {
            int undoCount = TerrainUndoSystem.GetUndoHistoryCount();
            string toolLabel = $"Undo Last Terrain Action ({undoCount}/15 states)";
            
            DebugTools.curTool = new DebugTool(toolLabel, delegate
            {
                // On click, execute undo
                if (TerrainUndoSystem.CanUndo())
                {
                    TerrainUndoSystem.ExecuteUndo();
                }
                else
                {
                    Log.Message("No undo data available for this map");
                }
            }, delegate
            {
                // On mouse over - show undo status and simple preview
                IntVec3 currentCell = UI.MouseCell();
                
                // Show simple cursor highlight
                if (currentCell.InBounds(Find.CurrentMap))
                {
                    Vector3 cellWorld = currentCell.ToVector3Shifted();
                    Vector2 cellScreen = cellWorld.MapToUIPosition();
                    
                    // Draw a distinctive undo cursor
                    DevGUI.DrawBox(new Rect(cellScreen.x - 15f, cellScreen.y - 15f, 30f, 30f), 2);
                }
            });
        }
    }
}
