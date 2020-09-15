
namespace WGJRoots
{
    public class SoilCell : Cell
    {
        public override CellType Type
        {
            get
            {
                if (IsHidden)
                {
                    return CellType.SoilHidden;
                }
                else
                {
                    return CellType.Soil;
                }
            }
        }

        public SoilCell(int x, int y)
            : base(x, y)
        {
        }
    }
}
