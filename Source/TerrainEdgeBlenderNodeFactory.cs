using System.Collections.Generic;
using LudeonTK;
using Verse;

namespace Verse
{
    public static class TerrainEdgeBlenderNodeFactory
    {
        public static List<DebugActionNode> CreateEdgeBlenderNodes(float brushRadius, float blendStrength)
        {
            List<DebugActionNode> list = new List<DebugActionNode>();

            // Create different blend strength options
            string strengthLabel = $"{(blendStrength * 100):F0}% blend";
            
            list.Add(new DebugActionNode($"Edge Blender ({strengthLabel})")
            {
                action = delegate
                {
                    TerrainEdgeBlenderToolFactory.CreateEdgeBlenderTool(brushRadius, blendStrength);
                }
            });

            return list;
        }
    }
}
