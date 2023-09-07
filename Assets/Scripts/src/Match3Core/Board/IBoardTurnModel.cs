using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Board
{
    public interface IBoardTurnModel
    {
        public Cell[,] GetCells();
        public bool ContainCoordinate(Coordinate coordinate);
        public bool GetCanHoldCell(Coordinate coordinate);
        public Cell GetCell(Coordinate coordinate);
    }
}