using System.Collections.Generic;

namespace Match3Core
{
    public interface ICellsFallModel
    {
        void FallAliveCellsInCollumn(int collumn, int lastDestroyedCell, int destroyedCellsCount = 1);

        public void RaiseDeadCell(int positionX, int positionY, int roof = 0);

        int GetColumnsCount();

        public string Print();
    }
}
