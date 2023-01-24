using Match3Core;

namespace Match3.Board
{
    public interface ISwitchCellsModel
    {
        public Cell GetCell(Coordinate coordinate);

        public bool GetCanHoldCell(Coordinate coordinate);

        public void SetCell(Cell cell, Coordinate coordinate);
    }
}