using LudeonTK;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainClearUndoToolFactory
    {
        public static void CreateClearUndoTool()
        {
            int undoCount = TerrainUndoSystem.GetUndoHistoryCount();
            string toolLabel = $"Clear Undo History ({undoCount} states)";
            
            DebugTools.curTool = new DebugTool(toolLabel, delegate
            {
                // On click, clear undo history
                TerrainUndoSystem.ClearUndoData();
                Log.Message("Undo history cleared");
            }, delegate
            {
                // On mouse over - show simple cursor
                IntVec3 currentCell = UI.MouseCell();
                
                // Show simple cursor highlight
                if (currentCell.InBounds(Find.CurrentMap))
                {
                    Vector3 cellWorld = currentCell.ToVector3Shifted();
                    Vector2 cellScreen = cellWorld.MapToUIPosition();
                    
                    // Draw a warning-style cursor for clear operation
                    DevGUI.DrawBox(new Rect(cellScreen.x - 10f, cellScreen.y - 10f, 20f, 20f), 1);
                }
            });
        }
    }
}
