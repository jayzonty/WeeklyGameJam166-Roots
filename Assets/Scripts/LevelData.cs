﻿using System.Collections.Generic;

using UnityEngine;

namespace WGJRoots
{
    public class LevelData
    {
        public delegate void LevelDataChanged(List<Vector3Int> changedCellsPositions);
        public event LevelDataChanged OnLevelDataChanged;

        public uint Width
        {
            get;
            private set;
        }

        public uint Height
        {
            get;
            private set;
        }

        private Cell[,] cells;

        public LevelData(uint width = 256, uint height = 64)
        {
            Width = width;
            Height = height;

            cells = new Cell[width, height];

            // For now, initialize the whole level data in the constructor.
            // Might want to make this asynchronous in the future.
            // Will also need to move this somewhere for random generation
            for (uint x = 0; x < width; ++x)
            {
                for (uint y = 0; y < height; ++y)
                {
                    cells[x, y] = new SoilCell();
                }
            }
        }

        public Cell GetCellAt(uint x, uint y)
        {
            if (((0 <= x) && (x < Width))
                && ((0 <= y) && (y < Height)))
            {
                return cells[x, y];
            }

            return null;
        }

        public void SetCellAt(uint x, uint y, Cell cell)
        {
            if (((0 <= x) && (x < Width))
                && ((0 <= y) && (y < Height)))
            {
                cells[x, y] = cell;

                List<Vector3Int> changedCellsPositions = new List<Vector3Int>();
                changedCellsPositions.Add(new Vector3Int((int)x, (int)y, 0));
                OnLevelDataChanged?.Invoke(changedCellsPositions);
            }
        }
    }
}
