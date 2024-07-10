using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;
using Match3Core;
using Match3Core.RandomGenerate;

namespace Match3Core
{
    public class FallingSidewayController : FallingController
    {
        public FallingSidewayController(IBoardFallLineModel fallLineModel, SwitchCellsContoller switchCellsContoller, Action updateView)
            : base(fallLineModel, switchCellsContoller, updateView)
        {
        }

        protected override Coordinate GetNextCell(Coordinate coordinate)
        {
            //Debug.Log($"{coordinate} 0 {CheckSideway(coordinate, 0)} -1 {CheckSideway(coordinate, -1)} 1 {CheckSideway(coordinate, 1)}");
            if (CheckSideway(coordinate, 0) != null)
            {
                return GetCellFromAbove(coordinate);
            }
            if (CheckSideway(coordinate, -1) != null) return CheckSideway(coordinate, -1);
            return CheckSideway(coordinate, 1);
        }

        private bool CheckWayToZeroCoordinate(Coordinate coordinate)
        {
            for (int x = coordinate.x; x >= 0; x--)
            {
                var nextCoordinate = new Coordinate(x, coordinate.y);
                if ((!_fallLineModel.GetCanHoldCell(nextCoordinate) 
                    && !_fallLineModel.GetCanPassCell(nextCoordinate))
                    || _fallLineModel.GetBlocked(nextCoordinate))
                {
                    return false;
                }
            }
            return true;
        }

        private Coordinate CheckSideway(Coordinate coordinate, int vector)
        {
            var nextCoordinate = new Coordinate(coordinate.x - 1, coordinate.y + vector);
            if (nextCoordinate.x == -1) return nextCoordinate;
            if (nextCoordinate.y == -1 || nextCoordinate.y > _fallLineModel.GetCollumns() - 1
                || (!_fallLineModel.GetCanHoldCell(nextCoordinate) && !_fallLineModel.GetCanPassCell(nextCoordinate)) 
                || (vector != 0 && !_fallLineModel.GetCanHoldCell(nextCoordinate))
                || _fallLineModel.GetBlocked(nextCoordinate)) return null;

            if (_fallLineModel.HasCell(nextCoordinate) || CheckWayToZeroCoordinate(nextCoordinate) 
                || CheckSideway(nextCoordinate, vector) != null) return nextCoordinate;
            return null;
        }
    }
}