using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WGJRoots
{
    public class LevelData
    {
        public class Cell
        {
            public enum CellType
            {
                Soil,

                RootBranch,
                RootBranchTip,

                // Nutrients
                Nutrient,

                // Obstacles
                Obstacle
            }

            public CellType cellType = CellType.Soil;
        }

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
            cells = new Cell[width, height];

            // For now, initialize the whole level data in the constructor.
            // Might want to make this asynchronous in the future.
            // Will also need to move this somewhere for random generation
            for (uint x = 0; x < width; ++x)
            {
                for (uint y = 0; y < height; ++y)
                {
                    cells[x, y] = new Cell();
                    cells[x, y].cellType = Cell.CellType.Soil;
                }
            }
        }
    }
}
