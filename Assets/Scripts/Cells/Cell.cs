using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WGJRoots
{
    public abstract class Cell
    {
        public enum CellType
        {
            Soil,

            RootBranchUpDown,
            RootBranchLeftRight,
            RootBranchUpLeft,
            RootBranchUpRight,
            RootBranchDownLeft,
            RootBranchDownRight,
            RootBranchUp,
            RootBranchDown,
            RootBranchLeft,
            RootBranchRight,

            // Nutrients
            Nutrient,

            // Obstacles
            Obstacle,

            Empty
        }

        public abstract CellType Type
        {
            get;
        }

        public int X
        {
            get;
            private set;
        }

        public int Y
        {
            get;
            private set;
        }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
