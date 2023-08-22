using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Match3Core.Board
{
    public interface ISwitchSlotModel
    {
        public Slot GetSlot(Coordinate coordinate);

        public void SetSlot(Coordinate coordinate, Slot slot);
    }
}