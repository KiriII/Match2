using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core
{
    public class ShapeConditionController
    {
        private const byte CONDITION = (byte)ConditionFlags.Shape;

        private HashSet<Coordinate> _shape;

        private event Action<byte> _condComplete;

        public ShapeConditionController(HashSet<Coordinate> shape, Action<byte> condComplete)
        {
            _shape = shape;

            _condComplete = condComplete;
        }

        public void ShapeChanged(HashSet<Coordinate> activeSlots)
        {
            //Debug.Log($"Shape = {String.Join(", ", _shape)} \nActiveSlots = {String.Join(", ", activeSlots)}");
            foreach (var slot in _shape)
            {
                //Debug.Log($"Slot {slot} contains {activeSlots.Contains(slot)}");
                if (activeSlots.Contains(slot)) Debug.Log($"Slot complete {slot}");
                if (!activeSlots.Contains(slot)) return;
            }
            _condComplete(CONDITION);
        }
    }
}