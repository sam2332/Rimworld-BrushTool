using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainEdgeBlender
    {
        private static System.Random random = new System.Random();
        
        public static void BlendTerrainEdges(IntVec3 center, float radius, float blendStrength = 0.3f)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            // Capture undo data before making changes
            TerrainUndoSystem.CaptureBeforeAction(center, radius);

            // Calculate the search area bounds
            int searchRadius = Mathf.CeilToInt(radius);
            
            // First pass: Find all terrain boundaries
            Dictionary<IntVec3, TerrainTransition> transitions = new Dictionary<IntVec3, TerrainTransition>();
            
            for (int x = -searchRadius; x <= searchRadius; x++)
            {
                for (int z = -searchRadius; z <= searchRadius; z++)
                {
                    IntVec3 cell = center + new IntVec3(x, 0, z);
                    
                    // Check if the cell is within the circular radius
                    float distance = Mathf.Sqrt(x * x + z * z);
                    if (distance <= radius && cell.InBounds(map))
                    {
                        var transition = AnalyzeTerrainTransition(cell, map);
                        if (transition != null)
                        {
                            transitions[cell] = transition;
                        }
                    }
                }
            }
            
            // Second pass: Apply blending based on detected transitions
            foreach (var kvp in transitions)
            {
                IntVec3 cell = kvp.Key;
                TerrainTransition transition = kvp.Value;
                
                // Apply blending based on transition strength and user-defined blend strength
                if (random.NextDouble() < (transition.TransitionStrength * blendStrength))
                {
                    // Choose the terrain to blend to based on surrounding terrain weights
                    TerrainDef terrainToApply = ChooseBlendTerrain(transition);
                    if (terrainToApply != null)
                    {
                        map.terrainGrid.SetTerrain(cell, terrainToApply);
                    }
                }
            }
        }
        
        private static TerrainTransition AnalyzeTerrainTransition(IntVec3 cell, Map map)
        {
            TerrainDef centerTerrain = map.terrainGrid.TerrainAt(cell);
            Dictionary<TerrainDef, int> neighboringTerrains = new Dictionary<TerrainDef, int>();
            
            // Check all 8 neighboring cells
            IntVec3[] neighbors = {
                cell + new IntVec3(-1, 0, -1), cell + new IntVec3(0, 0, -1), cell + new IntVec3(1, 0, -1),
                cell + new IntVec3(-1, 0, 0),                                   cell + new IntVec3(1, 0, 0),
                cell + new IntVec3(-1, 0, 1),  cell + new IntVec3(0, 0, 1),  cell + new IntVec3(1, 0, 1)
            };
            
            int differentTerrainCount = 0;
            foreach (IntVec3 neighbor in neighbors)
            {
                if (neighbor.InBounds(map))
                {
                    TerrainDef neighborTerrain = map.terrainGrid.TerrainAt(neighbor);
                    if (neighborTerrain != centerTerrain)
                    {
                        differentTerrainCount++;
                        if (neighboringTerrains.ContainsKey(neighborTerrain))
                        {
                            neighboringTerrains[neighborTerrain]++;
                        }
                        else
                        {
                            neighboringTerrains[neighborTerrain] = 1;
                        }
                    }
                }
            }
            
            // Only create transition if there are different terrains nearby
            if (differentTerrainCount > 0)
            {
                return new TerrainTransition
                {
                    CenterTerrain = centerTerrain,
                    NeighboringTerrains = neighboringTerrains,
                    TransitionStrength = (float)differentTerrainCount / 8.0f // Normalize to 0-1
                };
            }
            
            return null;
        }
        
        private static TerrainDef ChooseBlendTerrain(TerrainTransition transition)
        {
            // Weight selection based on neighboring terrain frequency
            var totalWeight = transition.NeighboringTerrains.Values.Sum();
            var randomValue = random.NextDouble() * totalWeight;
            
            double currentWeight = 0;
            foreach (var kvp in transition.NeighboringTerrains)
            {
                currentWeight += kvp.Value;
                if (randomValue <= currentWeight)
                {
                    return kvp.Key;
                }
            }
            
            // Fallback: return the most common neighboring terrain
            return transition.NeighboringTerrains.OrderByDescending(kvp => kvp.Value).First().Key;
        }
    }
    
    public class TerrainTransition
    {
        public TerrainDef CenterTerrain { get; set; }
        public Dictionary<TerrainDef, int> NeighboringTerrains { get; set; }
        public float TransitionStrength { get; set; } // 0-1, how much transition is needed
    }
}
