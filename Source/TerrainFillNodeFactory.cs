using System.Collections.Generic;
using System.Linq;
using LudeonTK;
using RimWorld;
using Verse;

namespace Verse
{
    public static class TerrainFillNodeFactory
    {
        public static List<DebugActionNode> CreateTerrainFillNodes()
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
                        TerrainFillToolFactory.CreateFillToolForTerrain(localTerrain);
                    }
                });
            }

            return list;
        }
    }
}
