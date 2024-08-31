
using System.Collections.Generic;

namespace Match3Core
{
    public interface IWinControllerModel
    {
        public HashSet<Cell> GetCellsInCoordinates(IEnumerable<Coordinate> coordinates);
    }
}