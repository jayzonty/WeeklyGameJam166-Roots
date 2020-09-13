using System.Collections.Generic;

using UnityEngine;

namespace WGJRoots
{
    public class LevelData
    {
        public delegate void LevelDataChanged(List<Vector3Int> changedCellsPositions);
        public event LevelDataChanged OnLevelDataChanged;

        public int Width
        {
            get;
            private set;
        }

        public int Height
        {
            get;
            private set;
        }

        private Cell[,] cells;

        public LevelData(int width = 256, int height = 64)
        {
            Width = width;
            Height = height;

            cells = new Cell[width, height];

            // For now, initialize the whole level data in the constructor.
            // Might want to make this asynchronous in the future.
            // Will also need to move this somewhere for random generation
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    cells[x, y] = new SoilCell(x, y);
                }
            }
        }

        public Cell GetCellAt(int x, int y)
        {
            if (((0 <= x) && (x < Width))
                && ((0 <= y) && (y < Height)))
            {
                return cells[x, y];
            }

            return null;
        }

        public void SetCellAt(int x, int y, Cell cell)
        {
            if (((0 <= x) && (x < Width))
                && ((0 <= y) && (y < Height)))
            {
                cells[x, y] = cell;

                List<Vector3Int> changedCellsPositions = new List<Vector3Int>();
                changedCellsPositions.Add(new Vector3Int(x, y, 0));
                OnLevelDataChanged?.Invoke(changedCellsPositions);
            }
        }
    }
}
