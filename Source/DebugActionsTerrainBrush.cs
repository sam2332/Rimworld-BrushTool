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
        
        
        
        [DebugAction("Map", "Terrain brush (single)", false, false, false, false, false, 91, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> TerrainBrushRadiusSingle()
        {
            return CreateTerrainBrushNodes(0f);
        }

        [DebugAction("Map", "Terrain brush (radius 1)", false, false, false, false, false, 91, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> TerrainBrushRadius1()
        {
            return CreateTerrainBrushNodes(1.0f);
        }
        
        [DebugAction("Map", "Terrain brush (radius 2)", false, false, false, false, false, 91, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> TerrainBrushRadius2()
        {
            return CreateTerrainBrushNodes(2.0f);
        }

        [DebugAction("Map", "Terrain brush (radius 4)", false, false, false, false, false, 90, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> TerrainBrushRadius4()
        {
            return CreateTerrainBrushNodes(4.0f);
        }

        [DebugAction("Map", "Terrain brush (radius 8)", false, false, false, false, false, 89, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> TerrainBrushRadius8()
        {
            return CreateTerrainBrushNodes(8.0f);
        }

        [DebugAction("Map", "Terrain brush (radius 16)", false, false, false, false, false, 88, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> TerrainBrushRadius16()
        {
            return CreateTerrainBrushNodes(16.0f);
        }

        [DebugAction("Map", "Terrain brush (radius 32)", false, false, false, false, false, 87, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> TerrainBrushRadius32()
        {
            return CreateTerrainBrushNodes(32.0f);
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

            // Draw circles to represent the brush area - draw multiple circles for better visibility
            Vector3 centerWorld = center.ToVector3Shifted();
            
            // Draw the outer circle
            GenDraw.DrawCircleOutline(centerWorld, radius, SimpleColor.White);
            
            // Draw a smaller inner circle for better visibility
            if (radius > 1f)
            {
                GenDraw.DrawCircleOutline(centerWorld, radius * 0.7f, SimpleColor.White);
            }
            
            // Also draw individual cell previews for affected cells
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
                        // Draw a subtle highlight on each affected cell
                        Vector3 cellCenter = cell.ToVector3Shifted();
                        GenDraw.DrawTargetHighlight(new LocalTargetInfo(cell));
                    }
                }
            }
        }
    }
}
