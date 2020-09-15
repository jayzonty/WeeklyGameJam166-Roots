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

            SoilHidden,

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

        public bool IsHidden
        {
            get;
            set;
        } = true;

        public bool IsDiggable
        {
            get;
            set;
        } = true;

        public virtual int BranchCost
        {
            get;
            set;
        } = 1;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
