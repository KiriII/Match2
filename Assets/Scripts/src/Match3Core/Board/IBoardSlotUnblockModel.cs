using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Board
{
    public interface IBoardSlotUnblockModel
    {
        public bool GetBlocked(Coordinate coordinate);
        public List<Coordinate> GetNeighbourSlot(Coordinate coordinate);
        public void UnblockSlot(Coordinate coordinate);
    }
}