
namespace WGJRoots
{
    public class ObstacleCell : Cell
    {
        public override CellType Type => CellType.Obstacle;

        public ObstacleCell(int x, int y, int branchCost = 3)
            : base(x, y)
        {
            BranchCost = branchCost;
        }
    }
}
