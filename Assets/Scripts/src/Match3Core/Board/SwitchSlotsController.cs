using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Board
{
    public class SwitchSlotsController
    {
        private ISwitchSlotModel _switchSlotsModel;

        private event Action _slotSwitched;

        public SwitchSlotsController(ISwitchSlotModel switchSlotModel)
        {
            _switchSlotsModel = switchSlotModel;
        }

        public void SwitchSlots(Coordinate switchCoordinate1, Coordinate switchCoordinate2)
        {
            var slot1 = new Slot(_switchSlotsModel.GetSlot(switchCoordinate1));
            var slot2 = new Slot(_switchSlotsModel.GetSlot(switchCoordinate2));

            _switchSlotsModel.SetSlot(switchCoordinate2, slot1);
            _switchSlotsModel.SetSlot(switchCoordinate1, slot2);

            OnSlotSwitched();
        }

        public void SwitchWithNewSlot(Coordinate coordinate, Slot newSlot)
        {
            _switchSlotsModel.SetSlot(coordinate, newSlot);

            OnSlotSwitched();
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
