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

        private Cell[,] backgroundCells;
        private Cell[,] foregroundCells;

        public LevelData(int width = 256, int height = 64)
        {
            Width = width;
            Height = height;

            backgroundCells = new Cell[width, height];
            foregroundCells = new Cell[width, height];

            // For now, initialize the whole level data in the constructor.
            // Might want to make this asynchronous in the future.
            // Will also need to move this somewhere for random generation
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    backgroundCells[x, y] = new SoilCell(x, y);

                    if (y < height - 3)
                    {
                        float nutrientRoll = Random.Range(0.0f, 1.0f);
                        if (nutrientRoll < 0.5f)
                        {
                            NutrientCell nutrientCell = new NutrientCell(x, y);
                            nutrientCell.NutrientValue = 2;
                            nutrientCell.BranchPointValue = 2;
                            foregroundCells[x, y] = nutrientCell;
                        }
                    }
                }
            }

            Cell seedCell = new EmptyCell(width / 2 - 1, height - 1);
            foregroundCells[seedCell.X, seedCell.Y] = seedCell;
            
            RootBranchCell leftRootBranch = new RootBranchCell(seedCell.X - 1, seedCell.Y, 0);
            leftRootBranch.SetParent(seedCell);
            foregroundCells[leftRootBranch.X, leftRootBranch.Y] = leftRootBranch;

            RootBranchCell rightRootBranch = new RootBranchCell(seedCell.X + 1, seedCell.Y, 1);
            rightRootBranch.SetParent(seedCell);
            foregroundCells[rightRootBranch.X, rightRootBranch.Y] = rightRootBranch;
        }

        public Cell GetForegroundCellAt(int x, int y)
        {
            if (((0 <= x) && (x < Width))
                && ((0 <= y) && (y < Height)))
            {
                return foregroundCells[x, y];
            }

            return null;
        }

        public void SetForegroundCellAt(int x, int y, Cell cell)
        {
            if (((0 <= x) && (x < Width))
                && ((0 <= y) && (y < Height)))
            {
                foregroundCells[x, y] = cell;

                List<Vector3Int> changedCellsPositions = new List<Vector3Int>();
                changedCellsPositions.Add(new Vector3Int(x, y, 0));
                OnLevelDataChanged?.Invoke(changedCellsPositions);
            }
        }

        public Cell GetBackgroundCellAt(int x, int y)
        {
            if (((0 <= x) && (x < Width))
                && ((0 <= y) && (y < Height)))
            {
                return backgroundCells[x, y];
            }

            return null;
        }

        public void SetBackgroundCellAt(int x, int y, Cell cell)
        {
            if (((0 <= x) && (x < Width))
                && ((0 <= y) && (y < Height)))
            {
                backgroundCells[x, y] = cell;

                List<Vector3Int> changedCellsPositions = new List<Vector3Int>();
                changedCellsPositions.Add(new Vector3Int(x, y, 0));
                OnLevelDataChanged?.Invoke(changedCellsPositions);
            }
        }
    }
}
