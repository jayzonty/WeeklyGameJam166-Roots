
namespace WGJRoots
{
    public class SoilCell : Cell
    {
        public override CellType Type
        {
            get
            {
                return CellType.Soil;
            }
        }

        public SoilCell(int x, int y)
            : base(x, y)
        {
        }
    }
}
