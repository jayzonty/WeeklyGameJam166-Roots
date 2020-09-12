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
    }
}
