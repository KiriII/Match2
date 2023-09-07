using Match3Core;

namespace Match3Core.Board
{
    public interface IBoardSwitchCellsModel
    {
        public Cell GetCell(Coordinate coordinate);

        public bool GetCanHoldCell(Coordinate coordinate);

        public bool GetBlocked(Coordinate coordinate);

        public void SetCell(Coordinate coordinate, Cell cell);
    }
}