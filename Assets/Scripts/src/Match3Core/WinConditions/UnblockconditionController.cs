using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core
{
    public class UnblockConditionController
    {
        private const byte CONDITION = (byte)ConditionFlags.Unblock;

        private int _count = 0;

        private event Action<byte> _condComplete;

        public UnblockConditionController(int count, Action<byte> condComplete)
        {
            _count = count;

            _condComplete = condComplete;   
        }

        public void SlotUnblocked(int count)
        {
            _count = _count - count;
            if (_count < 0)
            {
                Debug.LogWarning("Uncorrect blocked slots count");
            }
            if (_count <= 0)
            {
                _condComplete(CONDITION);
            }
        }
    }
}