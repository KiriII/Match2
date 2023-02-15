using Match3Core;

namespace Match3Core.Board
{
    public interface ISwitchCellsModel
    {
        public Cell GetCell(Coordinate coordinate);

        public bool GetCanHoldCell(Coordinate coordinate);

        public void SetCell(Coordinate coordinate, Cell cell);
    }
}