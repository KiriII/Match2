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
                Debug.Log("Unblock");
            }
            if ((flags & (byte)ConditionFlags.Special) > 0)
            {
                Debug.Log("Shape");
            }
        }

        public void CellsDestroyed(IEnumerable<Coordinate> destroyedCells)
        {
            _colorCount.CellsDestroyed(_boardModel.GetCellsInCoordinates(destroyedCells));
        }

        public void SpecialCellDestroyed(List<Coordinate> destroyedCells)
        {
            _special.SpecialCellDestroyed(destroyedCells.Count);
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