# Quick Terrain Brush Tool

This mod adds five developer brush tools to the Dev Palette for rapid terrain painting in RimWorld.

## Features

- **Five brush sizes**: 4×4, 8×8, 16×16, 32×32, 64×64
- **Cached terrain selection**: Choose a terrain type once and use it across all brush sizes
- **Visual preview**: See the brush area highlighted under your mouse cursor
- **Integrated workflow**: Uses the same terrain selection UI as the existing rectangle tool

## Usage

1. **Enable Development Mode** in RimWorld options
2. **Open Debug Menu** (usually F12 or the debug icon)
3. **Navigate to "Terrain" category** in the debug actions
4. **Select desired brush size**:
   - "Terrain Brush (4x4)"
   - "Terrain Brush (8x8)" 
   - "Terrain Brush (16x16)"
   - "Terrain Brush (32x32)"
   - "Terrain Brush (64x64)"
5. **Choose terrain type** from the menu (if not already cached)
6. **Click on the map** to paint terrain with the selected brush

## Tips

- The terrain selection is **remembered across all brush sizes** until you clear it or change it
- Use **"Clear cached terrain"** action to reset your terrain selection
- **Right-click** while using a brush tool to cancel it
- The brush preview shows exactly where terrain will be painted
- All brushes paint in perfect squares centered on the clicked cell

## Technical Details

- Reuses existing game logic from the terrain rectangle tool
- Uses the same terrain validation and map bounds checking
- Integrates seamlessly with RimWorld's debug action system
- No conflicts with existing terrain tools

## Installation

1. Copy the mod folder to your RimWorld Mods directory
2. Enable the mod in the mod manager
3. Restart RimWorld if needed
4. Enable Development Mode in options to access the tools
