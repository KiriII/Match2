using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;

namespace Match3Core
{
    public class SlotUnblockController
    {
        private IBoardSlotUnblockModel _slotUnblockBoard;

        private event Action<List<Coordinate>> _cellsUnblocked;
        private event Action<int> _countUnblocked;

        public SlotUnblockController(IBoardSlotUnblockModel slotUnblockBoard)
        {
            _slotUnblockBoard = slotUnblockBoard;
        }

        public void UnblockSlots(List<Coordinate> tripledCells)
        {
            var cellsNeighbours = new List<Coordinate>();
            foreach(var cell in tripledCells)
            {
                cellsNeighbours.AddRange(_slotUnblockBoard.GetNeighbourSlot(cell));
            }
            var blockedSlot = new List<Coordinate>(tripledCells);
            var unblockedCellCounter = 0;
            foreach(var cell in cellsNeighbours)
            {
                if (_slotUnblockBoard.GetBlocked(cell)) 
                {
                    _slotUnblockBoard.UnblockSlot(cell);
                    blockedSlot.Add(cell);
                    unblockedCellCounter++;
                }
            }
            //Debug.Log(String.Join(", ", blockedSlot));
            OnCellsUnblocked(blockedSlot);
            OnCountUnblocked(unblockedCellCounter);
        }

        private void OnCellsUnblocked(List<Coordinate> unblockedCells)
        {
            _cellsUnblocked?.Invoke(unblockedCells);
        }

        public void EnableCellUnblockedListener(Action<List<Coordinate>> methodInLitener)
        {
            _cellsUnblocked += methodInLitener;
        }

        public void DesableCellUnblockedListener(Action<List<Coordinate>> methodInLitener)
        {
            _cellsUnblocked -= methodInLitener;
        }

        private void OnCountUnblocked(int count)
        {
            _countUnblocked?.Invoke(count);
        }

        public void EnableCountUnblockedListener(Action<int> methodInLitener)
        {
            _countUnblocked += methodInLitener;
        }

        public void DesableCountUnblockedListener(Action<int> methodInLitener)
        {
            _countUnblocked -= methodInLitener;
        }
    }
}
