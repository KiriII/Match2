using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Board
{
    public interface IBoardSlotUnblockModel
    {
        public List<Coordinate> GetNeighbourSlot(Coordinate coordinate);
        public void UnblockSlot(Coordinate coordinate);
        public bool GetBlocked(Coordinate coordinate);
    }
}