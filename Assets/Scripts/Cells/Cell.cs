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

            RootBranchVertical,
            RootBranchHorizontal,
            RootBranchUpLeft,
            RootBranchUpRight,
            RootBranchDownLeft,
            RootBranchDownRight,
            RootBranchTipUp,
            RootBranchTipDown,
            RootBranchTipLeft,
            RootBranchTipRight,

            // Nutrients
            Nutrient,

            // Obstacles
            Obstacle
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
