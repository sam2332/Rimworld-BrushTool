using Verse;

namespace Verse
{
    public static class TerrainBrushState
    {
        // State tracking for paint-while-dragging
        public static bool IsPainting { get; set; } = false;
        public static IntVec3 LastPaintedCell { get; set; } = IntVec3.Invalid;
        public static TerrainDef CurrentTerrain { get; set; } = null;
        public static float CurrentBrushRadius { get; set; } = 0f;
        
        // State tracking for spray tools
        public static bool IsSpraying { get; set; } = false;
        public static IntVec3 LastSprayedCell { get; set; } = IntVec3.Invalid;
        public static float CurrentSprayCoverage { get; set; } = 0.1f;
    }
}
