using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;
using Match3Core;
using Match3Core.RandomGenerate;

namespace Match3Core.Falling
{
    public class FallingController : StealingCellsController
    {
        private IFallLineModel _fallLineModel;
        private SwitchCellsContoller _switchCellsContoller;

        public FallingController(IFallLineModel fallLineModel, SwitchCellsContoller switchCellsContoller)
        {
            _fallLineModel = fallLineModel;
            _switchCellsContoller = switchCellsContoller;
        }

        public void FallingWithDeadCells(List<Coordinate> deadCellsCoordinates)
        {
            foreach (Coordinate c in deadCellsCoordinates)
            {
                TrainOfSteals(c);
            }
        }

        public override void TrainOfSteals(Coordinate coordinate)
        {
            var currentCellCoordinate = coordinate;
            while (currentCellCoordinate.x >= 0)
            {
                //Debug.Log(currentCellCoordinate.x);
                var nextCellCoordinate = GetNextCell(currentCellCoordinate);
                if (nextCellCoordinate == null) break;
                if (nextCellCoordinate.x == -1)
                {
                    _switchCellsContoller.SwitchWithNewCell(currentCellCoordinate, RandomCellsGenerator.GenerateNewCell());
                    break;
                }

                _switchCellsContoller.SwitchCells(currentCellCoordinate, nextCellCoordinate);

                currentCellCoordinate = nextCellCoordinate;
            }
        }

        private Coordinate GetNextCell(Coordinate coordinate)
        {
            var newCoordinate = new Coordinate(coordinate.x - 1, coordinate.y);
            if (newCoordinate.x == -1) return newCoordinate;
            if (!_fallLineModel.GetCanHoldCell(newCoordinate))
            {
                if (_fallLineModel.GetCanPassCell(newCoordinate))
                {
                    return GetNextCell(newCoordinate);
                }
                else
                {
                    return null;
                }
            }
            return newCoordinate;
        }
    }
}