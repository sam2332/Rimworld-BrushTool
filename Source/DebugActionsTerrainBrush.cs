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
            DebugActionNode sprayPaintNode = new DebugActionNode("Spray Paint");
            
            // Light Spray Paint (5%)
            DebugActionNode lightSprayNode = new DebugActionNode("Light Spray (5%)");
            
            // Create parent node for Single cell light sprays
            DebugActionNode lightSpraySingleNode = new DebugActionNode("Single cell");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(0f, 0.05f))
            {
                lightSpraySingleNode.AddChild(terrain);
            }
            lightSprayNode.AddChild(lightSpraySingleNode);

            // Create parent node for Radius 1 light sprays
            DebugActionNode lightSprayRadius1Node = new DebugActionNode("Radius 1");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(1.0f, 0.05f))
            {
                lightSprayRadius1Node.AddChild(terrain);
            }
            lightSprayNode.AddChild(lightSprayRadius1Node);

            // Create parent node for Radius 2 light sprays
            DebugActionNode lightSprayRadius2Node = new DebugActionNode("Radius 2");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(2.0f, 0.05f))
            {
                lightSprayRadius2Node.AddChild(terrain);
            }
            lightSprayNode.AddChild(lightSprayRadius2Node);

            // Create parent node for Radius 4 light sprays
            DebugActionNode lightSprayRadius4Node = new DebugActionNode("Radius 4");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(4.0f, 0.05f))
            {
                lightSprayRadius4Node.AddChild(terrain);
            }
            lightSprayNode.AddChild(lightSprayRadius4Node);

            // Create parent node for Radius 8 light sprays
            DebugActionNode lightSprayRadius8Node = new DebugActionNode("Radius 8");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(8.0f, 0.05f))
            {
                lightSprayRadius8Node.AddChild(terrain);
            }
            lightSprayNode.AddChild(lightSprayRadius8Node);

            // Create parent node for Radius 16 light sprays
            DebugActionNode lightSprayRadius16Node = new DebugActionNode("Radius 16");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(16.0f, 0.05f))
            {
                lightSprayRadius16Node.AddChild(terrain);
            }
            lightSprayNode.AddChild(lightSprayRadius16Node);

            // Create parent node for Radius 32 light sprays
            DebugActionNode lightSprayRadius32Node = new DebugActionNode("Radius 32");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(32.0f, 0.05f))
            {
                lightSprayRadius32Node.AddChild(terrain);
            }
            lightSprayNode.AddChild(lightSprayRadius32Node);

            sprayPaintNode.AddChild(lightSprayNode);

            // Medium Spray Paint (10%)
            DebugActionNode mediumSprayNode = new DebugActionNode("Medium Spray (10%)");
            
            // Create parent node for Single cell medium sprays
            DebugActionNode mediumSpraySingleNode = new DebugActionNode("Single cell");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(0f, 0.1f))
            {
                mediumSpraySingleNode.AddChild(terrain);
            }
            mediumSprayNode.AddChild(mediumSpraySingleNode);

            // Create parent node for Radius 1 medium sprays
            DebugActionNode mediumSprayRadius1Node = new DebugActionNode("Radius 1");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(1.0f, 0.1f))
            {
                mediumSprayRadius1Node.AddChild(terrain);
            }
            mediumSprayNode.AddChild(mediumSprayRadius1Node);

            // Create parent node for Radius 2 medium sprays
            DebugActionNode mediumSprayRadius2Node = new DebugActionNode("Radius 2");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(2.0f, 0.1f))
            {
                mediumSprayRadius2Node.AddChild(terrain);
            }
            mediumSprayNode.AddChild(mediumSprayRadius2Node);

            // Create parent node for Radius 4 medium sprays
            DebugActionNode mediumSprayRadius4Node = new DebugActionNode("Radius 4");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(4.0f, 0.1f))
            {
                mediumSprayRadius4Node.AddChild(terrain);
            }
            mediumSprayNode.AddChild(mediumSprayRadius4Node);

            // Create parent node for Radius 8 medium sprays
            DebugActionNode mediumSprayRadius8Node = new DebugActionNode("Radius 8");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(8.0f, 0.1f))
            {
                mediumSprayRadius8Node.AddChild(terrain);
            }
            mediumSprayNode.AddChild(mediumSprayRadius8Node);

            // Create parent node for Radius 16 medium sprays
            DebugActionNode mediumSprayRadius16Node = new DebugActionNode("Radius 16");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(16.0f, 0.1f))
            {
                mediumSprayRadius16Node.AddChild(terrain);
            }
            mediumSprayNode.AddChild(mediumSprayRadius16Node);

            // Create parent node for Radius 32 medium sprays
            DebugActionNode mediumSprayRadius32Node = new DebugActionNode("Radius 32");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(32.0f, 0.1f))
            {
                mediumSprayRadius32Node.AddChild(terrain);
            }
            mediumSprayNode.AddChild(mediumSprayRadius32Node);

            sprayPaintNode.AddChild(mediumSprayNode);

            // Heavy Spray Paint (20%)
            DebugActionNode heavySprayNode = new DebugActionNode("Heavy Spray (20%)");
            
            // Create parent node for Single cell heavy sprays
            DebugActionNode heavySpraySingleNode = new DebugActionNode("Single cell");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(0f, 0.2f))
            {
                heavySpraySingleNode.AddChild(terrain);
            }
            heavySprayNode.AddChild(heavySpraySingleNode);

            // Create parent node for Radius 1 heavy sprays
            DebugActionNode heavySprayRadius1Node = new DebugActionNode("Radius 1");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(1.0f, 0.2f))
            {
                heavySprayRadius1Node.AddChild(terrain);
            }
            heavySprayNode.AddChild(heavySprayRadius1Node);

            // Create parent node for Radius 2 heavy sprays
            DebugActionNode heavySprayRadius2Node = new DebugActionNode("Radius 2");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(2.0f, 0.2f))
            {
                heavySprayRadius2Node.AddChild(terrain);
            }
            heavySprayNode.AddChild(heavySprayRadius2Node);

            // Create parent node for Radius 4 heavy sprays
            DebugActionNode heavySprayRadius4Node = new DebugActionNode("Radius 4");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(4.0f, 0.2f))
            {
                heavySprayRadius4Node.AddChild(terrain);
            }
            heavySprayNode.AddChild(heavySprayRadius4Node);

            // Create parent node for Radius 8 heavy sprays
            DebugActionNode heavySprayRadius8Node = new DebugActionNode("Radius 8");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(8.0f, 0.2f))
            {
                heavySprayRadius8Node.AddChild(terrain);
            }
            heavySprayNode.AddChild(heavySprayRadius8Node);

            // Create parent node for Radius 16 heavy sprays
            DebugActionNode heavySprayRadius16Node = new DebugActionNode("Radius 16");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(16.0f, 0.2f))
            {
                heavySprayRadius16Node.AddChild(terrain);
            }
            heavySprayNode.AddChild(heavySprayRadius16Node);

            // Create parent node for Radius 32 heavy sprays
            DebugActionNode heavySprayRadius32Node = new DebugActionNode("Radius 32");
            foreach (var terrain in TerrainSprayNodeFactory.CreateTerrainSprayNodes(32.0f, 0.2f))
            {
                heavySprayRadius32Node.AddChild(terrain);
            }
            heavySprayNode.AddChild(heavySprayRadius32Node);

            sprayPaintNode.AddChild(heavySprayNode);

            // Add the Spray Paint node to the main menu
            list.Add(sprayPaintNode);

            // Create parent node for Edge Blender tools
            DebugActionNode edgeBlenderNode = new DebugActionNode("Edge Blender Tools");
            
            // Add different blend strengths and radius options
            // Light blending (10%)
            DebugActionNode lightBlendNode = new DebugActionNode("Light Blend (10%)");
            foreach (var blenderTool in TerrainEdgeBlenderNodeFactory.CreateEdgeBlenderNodes(1.0f, 0.1f))
            {
                lightBlendNode.AddChild(new DebugActionNode("Radius 1") { action = blenderTool.action });
            }
            foreach (var blenderTool in TerrainEdgeBlenderNodeFactory.CreateEdgeBlenderNodes(2.0f, 0.1f))
            {
                lightBlendNode.AddChild(new DebugActionNode("Radius 2") { action = blenderTool.action });
            }
            foreach (var blenderTool in TerrainEdgeBlenderNodeFactory.CreateEdgeBlenderNodes(4.0f, 0.1f))
            {
                lightBlendNode.AddChild(new DebugActionNode("Radius 4") { action = blenderTool.action });
            }
            foreach (var blenderTool in TerrainEdgeBlenderNodeFactory.CreateEdgeBlenderNodes(8.0f, 0.1f))
            {
                lightBlendNode.AddChild(new DebugActionNode("Radius 8") { action = blenderTool.action });
            }
            edgeBlenderNode.AddChild(lightBlendNode);
            
            // Medium blending (30%)
            DebugActionNode mediumBlendNode = new DebugActionNode("Medium Blend (30%)");
            foreach (var blenderTool in TerrainEdgeBlenderNodeFactory.CreateEdgeBlenderNodes(1.0f, 0.3f))
            {
                mediumBlendNode.AddChild(new DebugActionNode("Radius 1") { action = blenderTool.action });
            }
            foreach (var blenderTool in TerrainEdgeBlenderNodeFactory.CreateEdgeBlenderNodes(2.0f, 0.3f))
            {
                mediumBlendNode.AddChild(new DebugActionNode("Radius 2") { action = blenderTool.action });
            }
            foreach (var blenderTool in TerrainEdgeBlenderNodeFactory.CreateEdgeBlenderNodes(4.0f, 0.3f))
            {
                mediumBlendNode.AddChild(new DebugActionNode("Radius 4") { action = blenderTool.action });
            }
            foreach (var blenderTool in TerrainEdgeBlenderNodeFactory.CreateEdgeBlenderNodes(8.0f, 0.3f))
            {
                mediumBlendNode.AddChild(new DebugActionNode("Radius 8") { action = blenderTool.action });
            }
            edgeBlenderNode.AddChild(mediumBlendNode);
            
            // Heavy blending (60%)
            DebugActionNode heavyBlendNode = new DebugActionNode("Heavy Blend (60%)");
            foreach (var blenderTool in TerrainEdgeBlenderNodeFactory.CreateEdgeBlenderNodes(1.0f, 0.6f))
            {
                heavyBlendNode.AddChild(new DebugActionNode("Radius 1") { action = blenderTool.action });
            }
            foreach (var blenderTool in TerrainEdgeBlenderNodeFactory.CreateEdgeBlenderNodes(2.0f, 0.6f))
            {
                heavyBlendNode.AddChild(new DebugActionNode("Radius 2") { action = blenderTool.action });
            }
            foreach (var blenderTool in TerrainEdgeBlenderNodeFactory.CreateEdgeBlenderNodes(4.0f, 0.6f))
            {
                heavyBlendNode.AddChild(new DebugActionNode("Radius 4") { action = blenderTool.action });
            }
            foreach (var blenderTool in TerrainEdgeBlenderNodeFactory.CreateEdgeBlenderNodes(8.0f, 0.6f))
            {
                heavyBlendNode.AddChild(new DebugActionNode("Radius 8") { action = blenderTool.action });
            }
            edgeBlenderNode.AddChild(heavyBlendNode);

            // Add the Edge Blender node to the main menu
            list.Add(edgeBlenderNode);

            // Create parent node for Line Tools
            DebugActionNode lineToolsNode = new DebugActionNode("Line Tools");
            
            // Add all terrain line tools
            foreach (var lineTool in TerrainLineNodeFactory.CreateTerrainLineNodes())
            {
                lineToolsNode.AddChild(lineTool);
            }

            // Add the Line Tools node to the main menu
            list.Add(lineToolsNode);

            // Create parent node for Fill Tools
            DebugActionNode fillToolsNode = new DebugActionNode("Fill Tools");
            
            // Add all terrain fill tools
            foreach (var fillTool in TerrainFillNodeFactory.CreateTerrainFillNodes())
            {
                fillToolsNode.AddChild(fillTool);
            }

            // Add the Fill Tools node to the main menu
            list.Add(fillToolsNode);

            // Create parent node for Rectangle Tools
            DebugActionNode rectToolsNode = new DebugActionNode("Rectangle Tools");
            
            // Add all terrain rectangle tools
            foreach (var rectTool in TerrainRectNodeFactory.CreateTerrainRectNodes())
            {
                rectToolsNode.AddChild(rectTool);
            }

            // Add the Rectangle Tools node to the main menu
            list.Add(rectToolsNode);

            // Create parent node for Undo Tools
            DebugActionNode undoToolsNode = new DebugActionNode("Undo Tools");
            
            // Add Undo Tool
            undoToolsNode.AddChild(new DebugActionNode("Undo Last Action")
            {
                action = delegate
                {
                    TerrainUndoToolFactory.CreateUndoTool();
                }
            });

            // Add Clear Undo History Tool
            undoToolsNode.AddChild(new DebugActionNode("Clear Undo History")
            {
                action = delegate
                {
                    TerrainClearUndoToolFactory.CreateClearUndoTool();
                }
            });

            // Add the Undo Tools node to the main menu
            list.Add(undoToolsNode);

            return list;
        }
    }
}
