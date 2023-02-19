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
        public bool GetCanHoldCell(int x, int y);
        public Cell GetCell(int x, int y);
    }
}
