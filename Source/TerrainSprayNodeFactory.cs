using System.Collections.Generic;
using System.Linq;
using LudeonTK;
using RimWorld;
using Verse;

namespace Verse
{
    public static class TerrainSprayNodeFactory
    {
        public static List<DebugActionNode> CreateTerrainSprayNodes(float brushRadius, float coverage = 0.1f)
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
                float localCoverage = coverage;
                
                list.Add(new DebugActionNode(localTerrain.LabelCap)
                {
                    action = delegate
                    {
                        TerrainSprayToolFactory.CreateSprayToolForTerrain(localTerrain, localBrushRadius, localCoverage);
                    }
                });
            }

            return list;
        }
    }
}
