using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;


namespace Match3Core.SlotManipulate
{
    public class SlotManipulateController
    {
        private SwitchSlotsController _switchSlotsController;
        private IBoardSlotManipulateModel _slotManipulateModel;

        private event Action<List<Coordinate>> _slotMoved;
        private event Action _updateView;

        public SlotManipulateController(SwitchSlotsController switchSlotsController, IBoardSlotManipulateModel slotManipulateModel, Action updateView)
        {
            _updateView = updateView;
            _switchSlotsController = switchSlotsController;
            _slotManipulateModel = slotManipulateModel;
        }

        public bool DestroySlot(Coordinate coordinate)
        {
            if (!_slotManipulateModel.GetCanDestroySlot(coordinate)) return false;

            _switchSlotsController.SwitchWithNewSlot(coordinate, 
                new Slot(coordinate, new Cell(CellsColor.Empty), false, true, false, true));
            OnViewUpdate();
            return true;
        }

        public bool CreateSlot(Coordinate coordinate)
        {
            if (_slotManipulateModel.GetCanHoldCell(coordinate) || !_slotManipulateModel.GetActive(coordinate)) return false;

            _switchSlotsController.SwitchWithNewSlot(coordinate,
                new Slot(coordinate, new Cell(CellsColor.Empty), true, true, false, true));
            OnViewUpdate();
            OnSlotMoved(new List<Coordinate> { coordinate});
            return true;
        }

        private void OnViewUpdate()
        {
            _updateView?.Invoke();
        }

        private void OnSlotMoved(List<Coordinate> destroyedCells)
        {
            _slotMoved?.Invoke(destroyedCells);
        }

        public void EnableSloMovedListener(Action<List<Coordinate>> methodInLitener)
        {
            _slotMoved += methodInLitener;
        }

        public void DesableSlotMovedListener(Action<List<Coordinate>> methodInLitener)
        {
            _slotMoved -= methodInLitener;
        }
    }
}