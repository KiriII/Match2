using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;
using Match3Core.Board;

namespace Match3Core
{
    public class ShockerController
    {
        private IBoardShockerModel _boardModel;

        private SlotManipulateController _slotManipulateController;
        private CellsDestroyController _cellsDestroyController;

        public ShockerController(IBoardShockerModel model, SlotManipulateController slotManipulateCtrl, CellsDestroyController _cellsDestroyCtrl)
        {
            _boardModel = model;

            _slotManipulateController = slotManipulateCtrl;
            _cellsDestroyController = _cellsDestroyCtrl;    
        }

        public bool CreateRowShocker(Coordinate coordinate, Slot slot)
        {
            if (_slotManipulateController.CreateSlot(coordinate, slot))
            {
                List<Coordinate> slotsInRow = _boardModel.GetCoordinateSlotsWithCellsInRow(coordinate);
                _cellsDestroyController.SimpleDestroyCells(slotsInRow);
                return true;
            }
            return false;
        }

        public bool CreateCollumnShocker(Coordinate coordinate, Slot slot)
        {
            return false;
        }
    }
}