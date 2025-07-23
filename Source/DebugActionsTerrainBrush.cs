using System.Collections.Generic;
using System.Linq;
using LudeonTK;
using RimWorld;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class DebugActionsTerrainBrush
    {
        // State tracking for paint-while-dragging
        private static bool isPainting = false;
        private static IntVec3 lastPaintedCell = IntVec3.Invalid;
        private static TerrainDef currentTerrain = null;
        private static float currentBrushRadius = 0f;
        
        [DebugAction("Map", "Brush Tool", false, false, false, false, false, 95, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> BrushToolMainMenu()
        {
            List<DebugActionNode> list = new List<DebugActionNode>();

            // Create parent node for Single cell brushes
            DebugActionNode singleNode = new DebugActionNode("Single cell");
            foreach (var terrain in CreateTerrainBrushNodes(0f))
            {
                singleNode.AddChild(terrain);
            }
            list.Add(singleNode);

            // Create parent node for Radius 1 brushes
            DebugActionNode radius1Node = new DebugActionNode("Radius 1");
            foreach (var terrain in CreateTerrainBrushNodes(1.0f))
            {
                radius1Node.AddChild(terrain);
            }
            list.Add(radius1Node);

            // Create parent node for Radius 2 brushes
            DebugActionNode radius2Node = new DebugActionNode("Radius 2");
            foreach (var terrain in CreateTerrainBrushNodes(2.0f))
            {
                radius2Node.AddChild(terrain);
            }
            list.Add(radius2Node);

            // Create parent node for Radius 4 brushes
            DebugActionNode radius4Node = new DebugActionNode("Radius 4");
            foreach (var terrain in CreateTerrainBrushNodes(4.0f))
            {
                radius4Node.AddChild(terrain);
            }
            list.Add(radius4Node);

            // Create parent node for Radius 8 brushes
            DebugActionNode radius8Node = new DebugActionNode("Radius 8");
            foreach (var terrain in CreateTerrainBrushNodes(8.0f))
            {
                radius8Node.AddChild(terrain);
            }
            list.Add(radius8Node);

            // Create parent node for Radius 16 brushes
            DebugActionNode radius16Node = new DebugActionNode("Radius 16");
            foreach (var terrain in CreateTerrainBrushNodes(16.0f))
            {
                radius16Node.AddChild(terrain);
            }
            list.Add(radius16Node);

            // Create parent node for Radius 32 brushes
            DebugActionNode radius32Node = new DebugActionNode("Radius 32");
            foreach (var terrain in CreateTerrainBrushNodes(32.0f))
            {
                radius32Node.AddChild(terrain);
            }
            list.Add(radius32Node);

            return list;
        }

        private static List<DebugActionNode> CreateTerrainBrushNodes(float brushRadius)
        {
            List<DebugActionNode> list = new List<DebugActionNode>();

            // Get all non-temporary terrain types, sorted by label
            var terrains = DefDatabase<TerrainDef>.AllDefs
                .Where(terr => !terr.temporary)
                .OrderBy(terr => terr.LabelCap.ToString());

            foreach (TerrainDef terrain in terrains)
            {
                TerrainDef localTerrain = terrain;
                float localBrushRadius = brushRadius;
                
                list.Add(new DebugActionNode(localTerrain.LabelCap)
                {
                    action = delegate
                    {
                        CreateBrushToolForTerrain(localTerrain, localBrushRadius);
                    }
                });
            }

            return list;
        }

        private static void CreateBrushToolForTerrain(TerrainDef terrain, float brushRadius)
        {
            // Store current tool settings for dragging
            currentTerrain = terrain;
            currentBrushRadius = brushRadius;
            
            // Create the brush tool with paint-while-dragging functionality
            string toolLabel = $"{terrain.LabelCap} brush (radius {brushRadius})";
            
            DebugTools.curTool = new DebugTool(toolLabel, delegate
            {
                // On click, start painting and paint the initial cell
                IntVec3 clickedCell = UI.MouseCell();
                isPainting = true;
                lastPaintedCell = clickedCell;
                ApplyTerrainBrush(clickedCell, terrain, brushRadius);
            }, delegate
            {
                // On mouse over (called every frame while tool is active)
                IntVec3 currentCell = UI.MouseCell();
                
                // Handle mouse release - stop painting
                if (isPainting && !UnityEngine.Input.GetMouseButton(0))
                {
                    isPainting = false;
                    lastPaintedCell = IntVec3.Invalid;
                }
                
                // Handle painting while dragging
                if (isPainting && UnityEngine.Input.GetMouseButton(0) && currentCell != lastPaintedCell)
                {
                    ApplyTerrainBrush(currentCell, terrain, brushRadius);
                    lastPaintedCell = currentCell;
                }
                
                // Always render brush preview
                RenderBrushPreview(currentCell, brushRadius);
            });
        }

        private static void ApplyTerrainBrush(IntVec3 center, TerrainDef terrain, float radius)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            // Calculate the search area bounds
            int searchRadius = Mathf.CeilToInt(radius);
            
            // Paint terrain in a circular pattern centered on the clicked cell
            for (int x = -searchRadius; x <= searchRadius; x++)
            {
                for (int z = -searchRadius; z <= searchRadius; z++)
                {
                    IntVec3 cell = center + new IntVec3(x, 0, z);
                    
                    // Check if the cell is within the circular radius
                    float distance = Mathf.Sqrt(x * x + z * z);
                    if (distance <= radius && cell.InBounds(map))
                    {
                        map.terrainGrid.SetTerrain(cell, terrain);
                    }
                }
            }
        }

        private static void RenderBrushPreview(IntVec3 center, float radius)
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
