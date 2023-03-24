using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Board
{
    public interface IGUIBoardModel
    {
        public int GetRows();
        public int GetCollumns();
        public Slot[,] GetSlots();
        public bool GetCanHoldCell(Coordinate coordinate);
        public bool GetBlocked(Coordinate coordinate);
        public Cell GetCell(Coordinate coordinate);
    }
}
