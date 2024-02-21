using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;
using Match3Core.FallenOff;

namespace Match3Core.SlotManipulate
{
    public class SlotManipulateController
    {
        private SwitchSlotsController _switchSlotsController;

        private IBoardSlotManipulateModel _slotManipulateModel;
        private FallenOffSlotsModel _fallenOffSlotsModel;

        private event Action<List<Coordinate>> _slotMoved;
        private event Action _updateView;

        public SlotManipulateController(SwitchSlotsController switchSlotsController, IBoardSlotManipulateModel slotManipulateModel, FallenOffSlotsModel fallenOffSlotsModel, Action updateView)
        {
            _updateView = updateView;
            _switchSlotsController = switchSlotsController;
            _slotManipulateModel = slotManipulateModel;
            _fallenOffSlotsModel = fallenOffSlotsModel;
        }

        public bool DestroySlot(Coordinate coordinate)
        {
            if (!_slotManipulateModel.GetCanDestroySlot(coordinate)) return false;

            _fallenOffSlotsModel.AddSlot(new Slot(_slotManipulateModel.GetSlot(coordinate)));
            _switchSlotsController.SwitchWithNewSlot(coordinate, 
                new Slot(coordinate, new Cell(CellsColor.Empty), false, true, false, true));
            
            OnViewUpdate();
            return true;
        }

        public bool CreateSlot(Coordinate coordinate, Slot slot)
        {
            if (_slotManipulateModel.GetCanHoldCell(coordinate) 
                || !_slotManipulateModel.GetActive(coordinate)) return false;
            if (!_fallenOffSlotsModel.Contains(slot))
            {
                return false;
            }
            _fallenOffSlotsModel.RemoveSlot(slot);
            _switchSlotsController.SwitchWithNewSlot(coordinate,
                new Slot(slot));
            OnViewUpdate();
            var emptySlots = new List<Coordinate> { };
            if (slot.Cell.Color == CellsColor.Empty)
            {
                emptySlots.Add(coordinate);
            }
            OnSlotMoved(emptySlots);
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