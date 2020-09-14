using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WGJRoots
{
    public class NutrientCell : Cell
    {
        public override CellType Type => CellType.Nutrient;

        public int NutrientValue
        {
            get;
            set;
        } = 1;

        public int BranchPointValue
        {
            get;
            set;
        } = 1;

        public NutrientCell(int x, int y)
            : base(x, y)
        {
        }
    }
}
