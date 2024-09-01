
using System.Collections.Generic;

namespace Match3Core
{
    public interface IBoardWinControllerModel
    {
        public HashSet<Cell> GetCellsInCoordinates(IEnumerable<Coordinate> coordinates);
    }
}