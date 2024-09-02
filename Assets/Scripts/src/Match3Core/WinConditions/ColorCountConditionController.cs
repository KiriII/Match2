using System;
using System.Collections.Generic;
using UnityEngine;


namespace Match3Core
{
    public class ColorCountConditionController
    {
        private const byte CONDITION = (byte)ConditionFlags.ColorCounter;

        private CellsColor _color;
        private int _count;

        private event Action<byte> _condComplete;

        public ColorCountConditionController(Condition condition, Action<byte> condComplete)
        {
            _color = condition.color;
            _count = condition.colorCount;

            _condComplete = condComplete;
        }

        public void CellsDestroyed(IEnumerable<Cell> cells)
        {
            if (_count <= 0) return;
            //Debug.Log(String.Join(',', cells));
            foreach (var c in cells)
            {
                if (c.Color == _color)
                {
                    _count--;
                    if (_count <= 0)
                    {
                        _condComplete(CONDITION);
                    }
                }
            }
            //Debug.Log($"Left destroy {_color} cells {_count}");
        }
    }
}