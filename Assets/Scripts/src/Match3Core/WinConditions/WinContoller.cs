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
        private ShapeConditionController _shape;

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
            byte flags = _condition.flags;
            if ((flags & (byte)ConditionFlags.ColorCounter) > 0) 
            {
                int count = conditions.colorCount;
                if (count <= 0)
                {
                    Debug.LogWarning($"Wrong color count {count}");
                    ConditionCompleted((byte)ConditionFlags.ColorCounter);
                }
                else
                {
                    _colorCount = new ColorCountConditionController(conditions, ConditionCompleted);
                }
            }
            if ((flags & (byte)ConditionFlags.Special) > 0)
            {
                if (_boxModel.GetSpecialCellsCount() <= 0)
                {
                    Debug.LogWarning($"There is no special cells on this level");
                    ConditionCompleted((byte)ConditionFlags.Special);
                } 
                else
                {
                    _special = new SpecialConditionController(_boxModel.GetSpecialCellsCount(), ConditionCompleted);
                }
            }
            if ((flags & (byte)ConditionFlags.Unblock) > 0)
            {
                if (_boardModel.GetBlockedCellCount() <= 0)
                {
                    Debug.LogWarning($"There is no unblock slots on this level");
                    ConditionCompleted((byte)ConditionFlags.Unblock);
                }
                else
                {
                    _unblock = new UnblockConditionController(_boardModel.GetBlockedCellCount(), ConditionCompleted);
                }
            }
            if ((flags & (byte)ConditionFlags.Shape) > 0)
            {
                if (conditions.shape.Count <= 0)
                {
                    Debug.LogWarning($"There is no shape condition in level config");
                    ConditionCompleted((byte)ConditionFlags.Shape);
                }
                else
                {
                    _shape = new ShapeConditionController(conditions.shape, ConditionCompleted);
                }
            }
            Debug.Log((ConditionFlags)(_condition.flags - _complitedFlags));
        }

        public void CellsDestroyed(IEnumerable<Coordinate> destroyedCells)
        {
            if (((_complitedFlags & (byte)ConditionFlags.ColorCounter) != 0) || (_colorCount == null)) return;

            _colorCount.CellsDestroyed(_boardModel.GetCellsInCoordinates(destroyedCells));
        }

        public void SpecialCellDestroyed(List<Coordinate> destroyedCells)
        {
            if (((_complitedFlags & (byte)ConditionFlags.Special) != 0) || (_special == null)) return;

            _special.SpecialCellDestroyed(destroyedCells.Count);
        }

        public void SlotUnblocked(int count)
        {
            if (((_complitedFlags & (byte)ConditionFlags.Unblock) != 0) || (_unblock == null)) return;

            _unblock.SlotUnblocked(count);
        }

        public void ShapeChanged(List<Coordinate> destroyedCells)
        {
            if (((_complitedFlags & (byte)ConditionFlags.Shape) != 0) || (_shape == null)) return;

            _shape.ShapeChanged(_boardModel.GetAllCanHoldCellCoordinates());
        }

        public void ConditionCompleted(byte cond)
        {
            Debug.Log(cond);
            _complitedFlags += cond;
            if (_complitedFlags == _condition.flags)
            {
                Debug.Log("Round won");
            }
        }
    }
}