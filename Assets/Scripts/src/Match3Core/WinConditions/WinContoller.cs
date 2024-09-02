using System;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core
{
    public class WinContoller
    {
        private readonly Condition _condition;
        private byte _complitedFlags;

        private IBoardWinControllerModel _boardModel;
        private IBoxWinConditionModel _boxModel;

        private ColorCountConditionController _colorCount;
        private SpecialConditionController _special;
        private UnblockConditionController _unblock;

        public WinContoller(Condition conditions, IBoardWinControllerModel boardModel, IBoxWinConditionModel boxModel)
        {
            _condition = conditions;
            //Debug.Log(conditions);

            _boardModel = boardModel;
            _boxModel = boxModel;

            InitiateConditionControllers(conditions);
        }

        private void InitiateConditionControllers(Condition conditions)
        {
            Debug.Log((ConditionFlags)_condition.flags);
            byte flags = _condition.flags;
            if ((flags & (byte)ConditionFlags.ColorCounter) > 0)
            {
                _colorCount = new ColorCountConditionController(conditions, ConditionCompleted);
            }
            if ((flags & (byte)ConditionFlags.Special) > 0)
            {
                _special = new SpecialConditionController(_boxModel.GetSpecialCellsCount(), ConditionCompleted);
            }
            if ((flags & (byte)ConditionFlags.Unblock) > 0)
            {
                _unblock = new UnblockConditionController(_boardModel.GetBlockedCellCount(), ConditionCompleted);
            }
            if ((flags & (byte)ConditionFlags.Shape) > 0)
            {
                Debug.Log("Shape");
            }
        }

        public void CellsDestroyed(IEnumerable<Coordinate> destroyedCells)
        {
            if ((_complitedFlags & (byte)ConditionFlags.ColorCounter) != 0) return;

            _colorCount.CellsDestroyed(_boardModel.GetCellsInCoordinates(destroyedCells));
        }

        public void SpecialCellDestroyed(List<Coordinate> destroyedCells)
        {
            if ((_complitedFlags & (byte)ConditionFlags.Special) != 0) return;

            _special.SpecialCellDestroyed(destroyedCells.Count);
        }

        public void SlotUnblocked(int count)
        {
            if ((_complitedFlags & (byte)ConditionFlags.Unblock) != 0) return;

            _unblock.SlotUnblocked(count);
        }

        public void ConditionCompleted(byte cond)
        {
            //Debug.Log(cond);
            _complitedFlags += cond;
            if (_complitedFlags == _condition.flags)
            {
                Debug.Log("Round won");
            }
        }
    }
}