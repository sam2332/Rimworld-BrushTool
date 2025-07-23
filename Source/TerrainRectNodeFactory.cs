using System.Collections.Generic;
using System.Linq;
using LudeonTK;
using RimWorld;
using Verse;

namespace Verse
{
    public static class TerrainRectNodeFactory
    {
        public static List<DebugActionNode> CreateTerrainRectNodes()
        {
            List<DebugActionNode> list = new List<DebugActionNode>();

            // Create parent nodes for filled and border rectangles
            DebugActionNode filledRectNode = new DebugActionNode("Filled Rectangles");
            DebugActionNode borderRectNode = new DebugActionNode("Border Only Rectangles");

            // Get all non-temporary terrain types, sorted by label
            var terrains = DefDatabase<TerrainDef>.AllDefs
                .Where(terr => !terr.temporary)
                .OrderBy(terr => terr.LabelCap.ToString());

            foreach (TerrainDef terrain in terrains)
            {
                TerrainDef localTerrain = terrain;
                
                // Add filled rectangle option
                filledRectNode.AddChild(new DebugActionNode(localTerrain.LabelCap)
                {
                    action = delegate
                    {
                        TerrainRectToolFactory.CreateRectToolForTerrain(localTerrain, true);
                    }
                });

                // Add border-only rectangle option
                borderRectNode.AddChild(new DebugActionNode(localTerrain.LabelCap)
                {
                    action = delegate
                    {
                        TerrainRectToolFactory.CreateRectToolForTerrain(localTerrain, false);
                    }
                });
            }

            list.Add(filledRectNode);
            list.Add(borderRectNode);

            return list;
        }
    }
}
