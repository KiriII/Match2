using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Board
{
    public interface ICheckTriplesBoardModel 
    {
        public Slot[,] GetSlots();
        public int GetRows();
        public int GetCollumns();
        public Cell[] GetFullRow(int rowNumber);
        public Cell[] GetFullCollumn(int collumnNumber);
    }
}

