using LudeonTK;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainFillToolFactory
    {
        public static void CreateFillToolForTerrain(TerrainDef terrain)
        {
            // Create the fill tool
            string toolLabel = $"{terrain.LabelCap} fill tool";
            
            DebugTools.curTool = new DebugTool(toolLabel, delegate
            {
                // On click, apply fill
                IntVec3 clickedCell = UI.MouseCell();
                TerrainFillLogic.ApplyTerrainFill(clickedCell, terrain);
            }, delegate
            {
                // On mouse over - show preview if reasonable size
                IntVec3 currentCell = UI.MouseCell();
                TerrainFillRenderer.RenderFillPreview(currentCell, terrain);
            });
        }
    }
}
