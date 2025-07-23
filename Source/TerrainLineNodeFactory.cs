using System.Collections.Generic;
using System.Linq;
using LudeonTK;
using RimWorld;
using Verse;

namespace Verse
{
    public static class TerrainLineNodeFactory
    {
        public static List<DebugActionNode> CreateTerrainLineNodes()
        {
            List<DebugActionNode> list = new List<DebugActionNode>();

            // Get all non-temporary terrain types, sorted by label
            var terrains = DefDatabase<TerrainDef>.AllDefs
                .Where(terr => !terr.temporary)
                .OrderBy(terr => terr.LabelCap.ToString());

            foreach (TerrainDef terrain in terrains)
            {
                TerrainDef localTerrain = terrain;
                
                list.Add(new DebugActionNode(localTerrain.LabelCap)
                {
                    action = delegate
                    {
                        TerrainLineToolFactory.CreateLineToolForTerrain(localTerrain);
                    }
                });
            }

            return list;
        }
    }
}
