using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Match3Core.Board
{
    public interface ISlotManipulateModel
    {
        public bool GetCanHoldCell(Coordinate coordinate);
        public bool GetActive(Coordinate coordinate);
    }
}