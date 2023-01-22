using Match3Core;

namespace Match3.Board
{
    public interface ISwitchCellsModel
    {
        public Cell GetCell(int x, int y);

        public void SetCell(Cell cell, int x, int y);
    }
}