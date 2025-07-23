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
        [DebugAction("Map", "Terrain brush (4x4)", false, false, false, false, false, 91, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> TerrainBrush4x4()
        {
            return CreateTerrainBrushNodes(4);
        }

        [DebugAction("Map", "Terrain brush (8x8)", false, false, false, false, false, 90, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> TerrainBrush8x8()
        {
            return CreateTerrainBrushNodes(8);
        }

        [DebugAction("Map", "Terrain brush (16x16)", false, false, false, false, false, 89, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> TerrainBrush16x16()
        {
            return CreateTerrainBrushNodes(16);
        }

        [DebugAction("Map", "Terrain brush (32x32)", false, false, false, false, false, 88, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> TerrainBrush32x32()
        {
            return CreateTerrainBrushNodes(32);
        }

        [DebugAction("Map", "Terrain brush (64x64)", false, false, false, false, false, 87, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> TerrainBrush64x64()
        {
            return CreateTerrainBrushNodes(64);
        }

        private static List<DebugActionNode> CreateTerrainBrushNodes(int brushSize)
        {
            List<DebugActionNode> list = new List<DebugActionNode>();

            // Get all non-temporary terrain types, sorted by label
            var terrains = DefDatabase<TerrainDef>.AllDefs
                .Where(terr => !terr.temporary)
                .OrderBy(terr => terr.LabelCap.ToString());

            foreach (TerrainDef terrain in terrains)
            {
                TerrainDef localTerrain = terrain;
                int localBrushSize = brushSize;
                
                list.Add(new DebugActionNode(localTerrain.LabelCap)
                {
                    action = delegate
                    {
                        CreateBrushToolForTerrain(localTerrain, localBrushSize);
                    }
                });
            }

            return list;
        }

        private static void CreateBrushToolForTerrain(TerrainDef terrain, int brushSize)
        {
            // Create the brush tool using the same pattern as DebugToolsGeneral.GenericRectTool
            string toolLabel = $"{terrain.LabelCap} brush ({brushSize}x{brushSize})";
            
            DebugTools.curTool = new DebugTool(toolLabel, delegate
            {
                IntVec3 clickedCell = UI.MouseCell();
                ApplyTerrainBrush(clickedCell, terrain, brushSize);
            }, delegate
            {
                // Render brush preview
                RenderBrushPreview(UI.MouseCell(), brushSize);
            });
        }

        private static void ApplyTerrainBrush(IntVec3 center, TerrainDef terrain, int brushSize)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            int half = brushSize / 2;

            // Paint terrain in a square pattern centered on the clicked cell
            for (int x = -half; x < half; x++)
            {
                for (int z = -half; z < half; z++)
                {
                    IntVec3 cell = center + new IntVec3(x, 0, z);
                    if (cell.InBounds(map))
                    {
                        map.terrainGrid.SetTerrain(cell, terrain);
                    }
                }
            }
        }

        private static void RenderBrushPreview(IntVec3 center, int brushSize)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            int half = brushSize / 2;

            // Calculate the bounds of the brush area
            IntVec3 min = center + new IntVec3(-half, 0, -half);
            IntVec3 max = center + new IntVec3(half - 1, 0, half - 1);

            // Clamp to map bounds
            min = min.ClampInsideMap(map);
            max = max.ClampInsideMap(map);

            // Create a CellRect for the brush area
            CellRect brushRect = new CellRect(min.x, min.z, max.x - min.x + 1, max.z - min.z + 1);

            // Draw the brush preview using the same rendering as the rectangle tool
            Vector3 v1 = new Vector3(brushRect.minX - 0.5f, 0f, brushRect.minZ - 0.5f);
            Vector3 v2 = new Vector3(brushRect.maxX + 0.5f, 0f, brushRect.maxZ + 0.5f);

            Vector2 screenPos1 = v1.MapToUIPosition();
            Vector2 screenPos2 = v2.MapToUIPosition();

            // Draw semi-transparent overlay
            Color previewColor = Color.white;
            previewColor.a = 0.3f;
            
            Rect previewRect = new Rect(
                screenPos1.x, 
                screenPos1.y, 
                screenPos2.x - screenPos1.x, 
                screenPos2.y - screenPos1.y
            );

            // Use DevGUI to draw the preview box
            GUI.color = previewColor;
            DevGUI.DrawBox(previewRect, 2);
            GUI.color = Color.white;
        }
    }
}
