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
            while (currentCellCoordinate.x > 0)
            {
                var nextCellCoordinate = GetNextCell(currentCellCoordinate);
                if (nextCellCoordinate == null) break;

                var nextCell = _fallLineModel.GetCell(nextCellCoordinate);
                if (nextCell.color == CellsColor.Empty) break;

                _fallLineModel.SetCell(currentCellCoordinate, nextCell);
                _fallLineModel.SetCell(nextCellCoordinate, new Cell(CellsColor));

                currentCellCoordinate = nextCellCoordinate;
            }
        }

        private Coordinate GetNextCell(Coordinate coordinate)
        {
            var newCoordinate = new Coordinate(coordinate.x, coordinate.y - 1);
            if (!_fallLineModel.GetCanHoldCell(newCoordinate)) return null;
            return newCoordinate;
        }
    }
}