using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Match3Core.Board
{
    public interface IBoardSlotManipulateModel
    {
        public bool GetCanHoldCell(Coordinate coordinate);
        public bool GetActive(Coordinate coordinate);
        public bool GetCanDestroySlot(Coordinate coordinate);
        public Slot GetSlot(Coordinate coordinate);
    }
}