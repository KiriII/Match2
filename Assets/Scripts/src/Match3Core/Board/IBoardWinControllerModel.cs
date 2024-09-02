
using System.Collections.Generic;

namespace Match3Core
{
    public interface IBoardWinControllerModel
    {
        public int GetBlockedCellCount();
        public HashSet<Cell> GetCellsInCoordinates(IEnumerable<Coordinate> coordinates);
        public HashSet<Coordinate> GetAllCanHoldCellCoordinates();
    }
}