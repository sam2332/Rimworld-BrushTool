# Quick Paint Terrain Brush Dev Tool

A comprehensive terrain painting toolkit for RimWorld map designers and developers. This mod provides advanced terrain painting tools including circular brushes, spray paint functionality, edge blending, line drawing, and flood fill capabilities for creating natural, varied terrain patterns.

## Features Overview

### 🎨 **Circular Brushes** - Full Coverage Terrain Painting
- **Seven brush sizes**: Single cell, radius 1, 2, 4, 8, 16, and 32
- **Perfect circular coverage** with radius-based positioning
- **Paint-while-dragging** functionality for continuous painting
- **Visual preview** with circular outline and cell highlights

### 🌿 **Spray Paint Tools** - Natural Terrain Variation
- **Light Spray Paint (5%)**: Ultra-sparse coverage for subtle terrain hints
- **Spray Paint (10%)**: Medium sparse coverage for natural terrain mixing  
- **Heavy Spray Paint (20%)**: Dense coverage for strong terrain variation
- **Randomized application** creates realistic, organic terrain patterns

### 🌅 **Edge Blender Tools** - Intelligent Terrain Transitions
- **Smart boundary detection** automatically finds terrain edges
- **Three blend strengths**: Light (10%), Medium (30%), Heavy (60%)
- **Multiple radius options**: 1, 2, 4, and 8 for different transition sizes
- **Contextual blending** creates smooth, natural terrain transitions

### 📏 **Line Tools** - Precision Terrain Drawing
- **Two-click interface**: Set start point, then end point to draw straight lines
- **All terrain types** supported with dedicated menu per terrain
- **Perfect straight lines** using Bresenham line algorithm
- **Visual preview** shows line path before confirming

### 🪣 **Fill Tools** - Flood Fill Connected Areas
- **Smart flood fill** changes all connected cells of the same terrain type
- **Performance limited** to 10,000 cells to prevent lag
- **Preview system** shows affected areas under 500 cells
- **All terrain types** supported for complete area transformation

## Menu Structure

The mod organizes all tools under **"Terrain Painter"** in the Debug Menu:

```
Terrain Painter
├── Brushes
│   ├── Single cell → [All terrain types]
│   ├── Radius 1 → [All terrain types]
│   ├── Radius 2 → [All terrain types]
│   ├── Radius 4 → [All terrain types]
│   ├── Radius 8 → [All terrain types]
│   ├── Radius 16 → [All terrain types]
│   └── Radius 32 → [All terrain types]
├── Light Spray Paint (5%)
│   ├── Single cell → [All terrain types]
│   ├── Radius 1 → [All terrain types]
│   └── [...] → [All terrain types]
├── Spray Paint (10%)
├── Heavy Spray Paint (20%)
├── Edge Blender Tools
│   ├── Light Blend (10%) → Radius 1, 2, 4, 8
│   ├── Medium Blend (30%) → Radius 1, 2, 4, 8
│   └── Heavy Blend (60%) → Radius 1, 2, 4, 8
├── Line Tools → [All terrain types]
└── Fill Tools → [All terrain types]
```

## Usage Guide

### Getting Started
1. **Enable Development Mode** in RimWorld options
2. **Open Debug Menu** (F12 or debug icon)
3. **Search for "Terrain Painter"** in the actions list
4. **Choose your tool type** from the hierarchical menu

### Using Brush and Spray Tools
1. Navigate to **Brushes** or any **Spray Paint** category
2. **Select brush radius** (Single cell to Radius 32)
3. **Choose terrain type** from the submenu
4. **Click and drag** on the map to paint terrain
5. **Right-click** to cancel the active tool

### Using Edge Blender Tools  
1. Navigate to **Edge Blender Tools**
2. **Select blend strength** (Light, Medium, or Heavy)
3. **Choose radius** (1, 2, 4, or 8)
4. **Click on terrain edges** to automatically blend transitions
5. The tool intelligently detects boundaries and creates smooth transitions

### Using Line Tools
1. Navigate to **Line Tools** 
2. **Select terrain type** from the menu
3. **Click first point** on the map to set line start
4. **Click second point** to set line end and draw the terrain line
5. **Right-click** to cancel before setting the second point

### Using Fill Tools
1. Navigate to **Fill Tools**
2. **Select terrain type** you want to fill with
3. **Click on any terrain area** to flood fill all connected cells of the same type
4. Limited to 10,000 cells for performance (preview shown for areas under 500 cells)

## Tips & Best Practices

- **Spray tools** create more natural, varied terrain than solid brushes
- **Edge blenders** work best on existing terrain boundaries for smooth transitions
- **Line tools** are perfect for roads, rivers, or geometric terrain features
- **Fill tools** excel at replacing large uniform areas quickly
- **Combine tools** for complex terrain design (e.g., brush base terrain, spray variations, blend edges)
- **Use smaller brushes** for detail work, larger for broad coverage
- **Preview systems** help visualize changes before applying

## Technical Features

- **Circular brush mathematics** for perfect radius-based coverage
- **Optimized rendering** with visual previews and highlighting
- **Smart flood fill algorithms** with performance safeguards
- **Hierarchical menu system** for organized tool access
- **Paint-while-dragging** for smooth, continuous coverage
- **No external dependencies** - uses only base RimWorld systems
- **Compatible with all terrain types** defined in the game

## Installation

1. **Subscribe on Steam Workshop** or download from GitHub
2. **Enable in mod manager** (no dependencies required)
3. **Restart RimWorld** to ensure proper loading
4. **Enable Development Mode** in options to access debug menu
5. **Find "Terrain Painter"** in the debug actions list

## Compatibility

- **RimWorld 1.6** fully supported
- **No known conflicts** with other mods
- **Development mode required** for access to tools
- **Works with modded terrains** automatically
