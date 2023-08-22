using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Board
{
    public interface ICellDestroyModel
    {
        public bool GetCanHoldCell(Coordinate coordinate);
    }
}