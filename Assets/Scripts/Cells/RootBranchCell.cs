using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WGJRoots
{
    public class RootBranchCell : Cell
    {
        public override CellType Type
        {
            get
            {
                bool up = false;
                bool down = false;
                bool left = false;
                bool right = false;
                
                // Get direction to the parent
                if (X < parent.X)
                {
                    right = true;
                }
                else if (parent.X < X)
                {
                    left = true;
                }

                if (Y < parent.Y)
                {
                    up = true;
                }
                else if (parent.Y < Y)
                {
                    down = true;
                }

                foreach (RootBranchCell child in children)
                {
                    if (child.X < X)
                    {
                        left = true;
                    }
                    else if (X < child.X)
                    {
                        right = true;
                    }

                    if (child.Y < Y)
                    {
                        down = true;
                    }
                    else if (Y < child.Y)
                    {
                        up = true;
                    }
                }

                string cellTypeName = "RootBranch";

                if (up)
                {
                    cellTypeName += "Up";
                }
                if (down)
                {
                    cellTypeName += "Down";
                }
                if (left)
                {
                    cellTypeName += "Left";
                }
                if (right)
                {
                    cellTypeName += "Right";
                }

                // TODO: Change depending on orientation of root branch, or if it's the tip
                CellType ret = CellType.Soil;
                System.Enum.TryParse(cellTypeName, out ret);
                return ret;
            }
        }

        public int Index
        {
            get;
            private set;
        }

        public Cell Parent
        {
            get
            {
                return parent;
            }
        }

        private List<RootBranchCell> children = new List<RootBranchCell>();
        private Cell parent = null;

        public RootBranchCell(int x, int y, int index)
            : base(x, y)
        {
            Index = index;
        }

        public void AddChild(RootBranchCell rootBranchCell)
        {
            children.Add(rootBranchCell);
        }

        public void RemoveChild(RootBranchCell rootBranchCell)
        {
            children.Remove(rootBranchCell);
        }

        public void SetParent(Cell parentCell)
        {
            parent = parentCell;
        }
    }
}
