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
        private ISlotManipulateModel _slotManipulateModel;

        private event Action<List<Coordinate>> _slotMoved;
        private event Action _updateView;

        public SlotManipulateController(SwitchSlotsController switchSlotsController, ISlotManipulateModel slotManipulateModel, Action updateView)
        {
            _updateView = updateView;
            _switchSlotsController = switchSlotsController;
            _slotManipulateModel = slotManipulateModel;
        }

        public bool DestroySlot(Coordinate coordinate)
        {
            if (!_slotManipulateModel.GetCanHoldCell(coordinate)) return false;

            _switchSlotsController.SwitchWithNewSlot(coordinate, 
                new Slot(coordinate, false, true, false, new Cell(CellsColor.Empty)));
            OnViewUpdate();
            return true;
        }

        public bool CreateSlot(Coordinate coordinate)
        {
            if (_slotManipulateModel.GetCanHoldCell(coordinate)) return false;

            _switchSlotsController.SwitchWithNewSlot(coordinate,
                new Slot(coordinate, true, true, false, new Cell(CellsColor.Empty)));
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