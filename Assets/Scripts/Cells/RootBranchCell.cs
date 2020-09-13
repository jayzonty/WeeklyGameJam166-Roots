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
                // TODO: Change depending on orientation of root branch, or if it's the tip
                return CellType.RootBranchVertical;
            }
        }

        public int Index
        {
            get;
            private set;
        }

        private List<RootBranchCell> children = new List<RootBranchCell>();

        public RootBranchCell(int x, int y, int index)
            : base(x, y)
        {
            Index = index;
        }
    }
}
