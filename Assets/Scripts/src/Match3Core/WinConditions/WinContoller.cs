using System;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core
{
    public class WinContoller
    {
        private readonly Condition _condition;
        private byte _complitedFlags;

        private IWinControllerModel _boardModel;

        private ColorCountConditionController _colorCount;
        public WinContoller(Condition conditions, IWinControllerModel boardModel)
        {
            _condition = conditions;
            Debug.Log(conditions);

            _boardModel = boardModel;

            _colorCount = new ColorCountConditionController(conditions, ConditionCompleted);
        }

        public void CellsDestroyed(IEnumerable<Coordinate> destroyedCells)
        {
            _colorCount.CellsDestroyed(_boardModel.GetCellsInCoordinates(destroyedCells));
        }

        public void ConditionCompleted(byte cond)
        {
            _complitedFlags += cond;
            if (_complitedFlags == _condition.flags)
            {
                Debug.Log("Round won");
            }
        }
    }

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
            Debug.Log(String.Join(',', cells));
            foreach (var c in cells)
            {
                if (c.Color == _color)
                {
                    _count--;
                    if (_count == 0)
                    {
                        _condComplete(CONDITION);
                    }
                }
            }
            Debug.Log($"Left destroy {_color} cells {_count}");
        }
    }
}