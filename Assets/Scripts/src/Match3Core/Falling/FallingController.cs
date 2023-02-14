using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3.Board;
using Match3Core;

namespace Match3.Falling
{
    public class FallingController : StealingCellsController
    {
        private IFallLineModel _fallLineModel;

        public FallingController(IFallLineModel fallLineModel)
        {
            _fallLineModel = fallLineModel;
        }

        public void FallingWithDeadCells(List<Coordinate> deadCellsCoordinates)
        {
            foreach (Coordinate c in deadCellsCoordinates)
            {
                TrainOfSteals(_fallLineModel, c);
            }
        }

        public override void TrainOfSteals(IFallLineModel fallLineModel, Coordinate coordinate)
        {
            var currentCellCoordinate = coordinate;
            while (currentCellCoordinate.x >= 0)
            {
                //Debug.Log(currentCellCoordinate.x);
                var nextCellCoordinate = GetNextCell(currentCellCoordinate);
                if (nextCellCoordinate == null) break;
                if (nextCellCoordinate.x == -1)
                {
                    _fallLineModel.SetCell(currentCellCoordinate, GenerateNewCell());
                    break;
                }

                var nextCell = _fallLineModel.GetCell(nextCellCoordinate);
                if (nextCell.color == CellsColor.Empty) break;

                _fallLineModel.SetCell(currentCellCoordinate, nextCell);
                _fallLineModel.SetCell(nextCellCoordinate, new Cell(CellsColor.Empty));

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

        private Cell GenerateNewCell()
        {
            return new Cell(CellsColor.Blue);
        }
    }
}