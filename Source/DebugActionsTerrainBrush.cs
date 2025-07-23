using System.Collections.Generic;
using LudeonTK;
using Verse;

namespace Verse
{
    public static class DebugActionsTerrainBrush
    {
        [DebugAction("Terrain Painter", null, false, false, false, false, false, 95, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static List<DebugActionNode> TerrainPainterMainMenu()
        {
            List<DebugActionNode> list = new List<DebugActionNode>();

            // Create parent node for Brushes containing all brush tools
            DebugActionNode brushesNode = new DebugActionNode("Brushes");
            
            // Add all brush radius options under the Brushes submenu
            // Create parent node for Single cell brushes
            DebugActionNode singleNode = new DebugActionNode("Single cell");
            foreach (var terrain in TerrainBrushNodeFactory.CreateTerrainBrushNodes(0f))
            {
                singleNode.AddChild(terrain);
            }
            brushesNode.AddChild(singleNode);

            // Create parent node for Radius 1 brushes
            DebugActionNode radius1Node = new DebugActionNode("Radius 1");
            foreach (var terrain in TerrainBrushNodeFactory.CreateTerrainBrushNodes(1.0f))
            {
                radius1Node.AddChild(terrain);
            }
            brushesNode.AddChild(radius1Node);

            // Create parent node for Radius 2 brushes
            DebugActionNode radius2Node = new DebugActionNode("Radius 2");
            foreach (var terrain in TerrainBrushNodeFactory.CreateTerrainBrushNodes(2.0f))
            {
                radius2Node.AddChild(terrain);
            }
            brushesNode.AddChild(radius2Node);

            // Create parent node for Radius 4 brushes
            DebugActionNode radius4Node = new DebugActionNode("Radius 4");
            foreach (var terrain in TerrainBrushNodeFactory.CreateTerrainBrushNodes(4.0f))
            {
                radius4Node.AddChild(terrain);
            }
            brushesNode.AddChild(radius4Node);

            // Create parent node for Radius 8 brushes
            DebugActionNode radius8Node = new DebugActionNode("Radius 8");
            foreach (var terrain in TerrainBrushNodeFactory.CreateTerrainBrushNodes(8.0f))
            {
                radius8Node.AddChild(terrain);
            }
            brushesNode.AddChild(radius8Node);

            // Create parent node for Radius 16 brushes
            DebugActionNode radius16Node = new DebugActionNode("Radius 16");
            foreach (var terrain in TerrainBrushNodeFactory.CreateTerrainBrushNodes(16.0f))
            {
                radius16Node.AddChild(terrain);
            }
            brushesNode.AddChild(radius16Node);

            // Create parent node for Radius 32 brushes
            DebugActionNode radius32Node = new DebugActionNode("Radius 32");
            foreach (var terrain in TerrainBrushNodeFactory.CreateTerrainBrushNodes(32.0f))
            {
                radius32Node.AddChild(terrain);
            }
            brushesNode.AddChild(radius32Node);

            // Add the Brushes node to the main menu
            list.Add(brushesNode);

            // Create parent node for Spray Paint containing all spray tools
            DebugActionNode sprayNode = new DebugActionNode("Spray Paint (10%)");
            
            // Add all spray radius options under the Spray Paint submenu
            // Create parent node for Single cell sprays
            DebugActionNode spraySingleNode = new DebugActionNode("Single cell");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(0f, 0.1f))
            {
                spraySingleNode.AddChild(terrain);
            }
            sprayNode.AddChild(spraySingleNode);

            // Create parent node for Radius 1 sprays
            DebugActionNode sprayRadius1Node = new DebugActionNode("Radius 1");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(1.0f, 0.1f))
            {
                sprayRadius1Node.AddChild(terrain);
            }
            sprayNode.AddChild(sprayRadius1Node);

            // Create parent node for Radius 2 sprays
            DebugActionNode sprayRadius2Node = new DebugActionNode("Radius 2");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(2.0f, 0.1f))
            {
                sprayRadius2Node.AddChild(terrain);
            }
            sprayNode.AddChild(sprayRadius2Node);

            // Create parent node for Radius 4 sprays
            DebugActionNode sprayRadius4Node = new DebugActionNode("Radius 4");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(4.0f, 0.1f))
            {
                sprayRadius4Node.AddChild(terrain);
            }
            sprayNode.AddChild(sprayRadius4Node);

            // Create parent node for Radius 8 sprays
            DebugActionNode sprayRadius8Node = new DebugActionNode("Radius 8");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(8.0f, 0.1f))
            {
                sprayRadius8Node.AddChild(terrain);
            }
            sprayNode.AddChild(sprayRadius8Node);

            // Create parent node for Radius 16 sprays
            DebugActionNode sprayRadius16Node = new DebugActionNode("Radius 16");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(16.0f, 0.1f))
            {
                sprayRadius16Node.AddChild(terrain);
            }
            sprayNode.AddChild(sprayRadius16Node);

            // Create parent node for Radius 32 sprays
            DebugActionNode sprayRadius32Node = new DebugActionNode("Radius 32");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(32.0f, 0.1f))
            {
                sprayRadius32Node.AddChild(terrain);
            }
            sprayNode.AddChild(sprayRadius32Node);

            // Add the Spray Paint node to the main menu
            list.Add(sprayNode);

            return list;
        }
    }
}
