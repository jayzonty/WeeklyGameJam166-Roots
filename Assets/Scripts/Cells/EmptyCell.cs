using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WGJRoots
{
    public class EmptyCell : Cell
    {
        public override CellType Type => CellType.Empty;

        public EmptyCell(int x, int y)
            : base(x, y)
        {
        }
    }
}
