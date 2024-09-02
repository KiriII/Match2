using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Board
{
    public class SwitchSlotsController
    {
        private IBoardSwitchSlotModel _switchSlotsModel;

        private event Action _slotSwitched;
        private event Action _updateView;

        public SwitchSlotsController(IBoardSwitchSlotModel switchSlotModel, Action updateView)
        {
            _switchSlotsModel = switchSlotModel;
            _updateView = updateView;
        }

        public void SwitchSlots(Coordinate switchCoordinate1, Coordinate switchCoordinate2)
        {
            var slot1 = new Slot(_switchSlotsModel.GetSlot(switchCoordinate1));
            var coordinate1 = slot1.Coordinate;
            var slot2 = new Slot(_switchSlotsModel.GetSlot(switchCoordinate2));
            var coordinate2 = slot2.Coordinate;

            slot1.Coordinate = coordinate2;
            slot2.Coordinate = coordinate1;

            _switchSlotsModel.SetSlot(switchCoordinate2, slot1);
            _switchSlotsModel.SetSlot(switchCoordinate1, slot2);

            OnViewUpdate();
            OnSlotSwitched();
        }

        public void SwitchWithNewSlot(Coordinate coordinate, Slot newSlot, bool instaTriples = true)
        {
            newSlot.Coordinate = coordinate;
            _switchSlotsModel.SetSlot(coordinate, newSlot);

            OnViewUpdate();
            if (instaTriples) OnSlotSwitched();
        }

        private void OnViewUpdate()
        {
            _updateView?.Invoke();
        }

        private void OnSlotSwitched()
        {
            _slotSwitched?.Invoke();
        }

        public void EnableSlotSwitchedListener(Action methodInLitener)
        {
            _slotSwitched += methodInLitener;
        }

        public void DesableSlotSwitchedListener(Action methodInLitener)
        {
            _slotSwitched -= methodInLitener;
        }
    }
}
