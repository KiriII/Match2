using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core
{
    public class SpecialConditionController
    {
        private const byte CONDITION = (byte)ConditionFlags.Special;

        private int _count;

        private event Action<byte> _condComplete;

        public SpecialConditionController(int count, Action<byte> condComplete)
        {
            _count = count;
            _condComplete = condComplete;
        }

        public void SpecialCellDestroyed(int count)
        {
            if (_count <= 0) throw new Exception("Wrong number of special cells");
            _count = _count - count;
            if (_count == 0)
            {
                _condComplete(CONDITION);
            }
        }
    }
}