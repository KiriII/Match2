using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Board
{
    public interface IBoardCellDestroyModel
    {
        public bool GetCanHoldCell(Coordinate coordinate);
        public bool GetCanDestroyCell(Coordinate coordinate);
    }
}