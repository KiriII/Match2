using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Board
{
    public interface IBoardCheckTriplesModel
    {
        public int GetRows();
        public int GetCollumns();
        public Cell[,] GetCells();
    }
}

