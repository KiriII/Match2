using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core
{
    public class FallenOffSlotsModel 
    {
        private List<Slot> _fallenSlots;

        public FallenOffSlotsModel()
        {
            _fallenSlots = new List<Slot>();
        }

        public FallenOffSlotsModel(List<Slot> fallenSlots)
        {
            _fallenSlots = new List<Slot>(fallenSlots);
        }

        public void AddSlot(Slot slot)
        {
            _fallenSlots.Add(slot);
            Debug.Log("Add Slot " + string.Join(", ", _fallenSlots));
        }

        public void RemoveSlot(Slot slot)
        {
            _fallenSlots.Remove(slot);
            Debug.Log("FallenOff " + string.Join(", ", _fallenSlots));
        }

        public bool Contains(Slot slot)
        {
            return _fallenSlots.Contains(slot);
        }

        public Slot GetFallenSlot()
        {
            return _fallenSlots[_fallenSlots.Count - 1];
        }
    }
}