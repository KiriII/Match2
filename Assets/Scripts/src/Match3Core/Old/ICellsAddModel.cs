using System.Collections.Generic;

namespace Match3Core
{
    public interface ICellsAddModel
    {
        void AddNewCellToCollumn(int collumn, Cell newCell);

        void AddNewBunchOffCellsToCollumn(int collumn, List<Cell> newCells);

        int GetColumnsCount();

        int GetRowsCount();

        Cell GetCell(int x, int y);

        void SetCellColorInPosition(int collumn, int row, CellsColor color);

        void SetCellInPosition(int collumn, int row, Cell cell);

        public string Print();
    }
}
