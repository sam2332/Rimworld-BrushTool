using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Verse
{
    public static class TerrainUndoSystem
    {
        private static List<UndoSnapshot> undoHistory = new List<UndoSnapshot>();
        private static readonly int MAX_UNDO_STATES = 15;

        public static void CaptureBeforeAction(List<IntVec3> cells)
        {
            Map map = Find.CurrentMap;
            if (map == null || cells == null || cells.Count == 0) return;

            // Create new snapshot
            UndoSnapshot newSnapshot = new UndoSnapshot
            {
                cellData = new Dictionary<IntVec3, TerrainDef>(),
                mapId = map.uniqueID,
                timestamp = System.DateTime.Now
            };

            // Capture current terrain state for all affected cells
            foreach (IntVec3 cell in cells)
            {
                if (cell.InBounds(map))
                {
                    newSnapshot.cellData[cell] = map.terrainGrid.TerrainAt(cell);
                }
            }

            // Add to history
            undoHistory.Add(newSnapshot);

            // Remove oldest entries if we exceed the limit
            while (undoHistory.Count > MAX_UNDO_STATES)
            {
                undoHistory.RemoveAt(0);
            }

            Log.Message($"Captured terrain state for {newSnapshot.cellData.Count} cells (History: {undoHistory.Count}/{MAX_UNDO_STATES})");
        }

        public static void CaptureBeforeAction(IntVec3 center, float radius)
        {
            Map map = Find.CurrentMap;
            if (map == null) return;

            List<IntVec3> cells = GetCellsInRadius(center, radius, map);
            CaptureBeforeAction(cells);
        }

        public static bool CanUndo()
        {
            Map map = Find.CurrentMap;
            if (map == null || undoHistory.Count == 0) return false;

            // Check if the most recent snapshot is for the current map
            UndoSnapshot mostRecent = undoHistory[undoHistory.Count - 1];
            return mostRecent.mapId == map.uniqueID;
        }

        public static void ExecuteUndo()
        {
            if (!CanUndo())
            {
                Log.Warning("No undo data available or map mismatch");
                return;
            }

            Map map = Find.CurrentMap;
            
            // Get the most recent snapshot
            UndoSnapshot snapshotToRestore = undoHistory[undoHistory.Count - 1];
            int restoredCount = 0;

            // Restore terrain for all captured cells
            foreach (var kvp in snapshotToRestore.cellData)
            {
                IntVec3 cell = kvp.Key;
                TerrainDef originalTerrain = kvp.Value;

                if (cell.InBounds(map))
                {
                    map.terrainGrid.SetTerrain(cell, originalTerrain);
                    restoredCount++;
                }
            }

            // Remove the snapshot we just restored
            undoHistory.RemoveAt(undoHistory.Count - 1);

            Log.Message($"Undo completed: restored {restoredCount} cells (History: {undoHistory.Count}/{MAX_UNDO_STATES})");
        }

        public static void ClearUndoData()
        {
            undoHistory.Clear();
        }

        private static List<IntVec3> GetCellsInRadius(IntVec3 center, float radius, Map map)
        {
            List<IntVec3> cells = new List<IntVec3>();
            
            if (radius <= 0f)
            {
                // Single cell case
                if (center.InBounds(map))
                {
                    cells.Add(center);
                }
                return cells;
            }

            // Multi-cell radius case
            int searchRadius = Mathf.CeilToInt(radius);
            for (int x = -searchRadius; x <= searchRadius; x++)
            {
                for (int z = -searchRadius; z <= searchRadius; z++)
                {
                    IntVec3 cell = center + new IntVec3(x, 0, z);
                    
                    // Check if the cell is within the circular radius
                    float distance = Mathf.Sqrt(x * x + z * z);
                    if (distance <= radius && cell.InBounds(map))
                    {
                        cells.Add(cell);
                    }
                }
            }

            return cells;
        }

        public static string GetUndoStatusText()
        {
            if (!CanUndo())
            {
                return "No undo data available";
            }

            UndoSnapshot mostRecent = undoHistory[undoHistory.Count - 1];
            return $"Undo available: {mostRecent.cellData.Count} cells ({undoHistory.Count}/{MAX_UNDO_STATES} history states)";
        }

        public static int GetUndoHistoryCount()
        {
            Map map = Find.CurrentMap;
            if (map == null) return 0;

            // Count snapshots for current map
            int count = 0;
            foreach (var snapshot in undoHistory)
            {
                if (snapshot.mapId == map.uniqueID)
                {
                    count++;
                }
            }
            return count;
        }

        public static void CleanupForMap(int mapId)
        {
            // Remove undo data for a specific map (useful when maps are destroyed)
            undoHistory.RemoveAll(snapshot => snapshot.mapId == mapId);
        }
    }

    public class UndoSnapshot
    {
        public Dictionary<IntVec3, TerrainDef> cellData;
        public int mapId;
        public System.DateTime timestamp;
    }
}
