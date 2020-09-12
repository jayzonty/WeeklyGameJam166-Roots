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
    }
}
