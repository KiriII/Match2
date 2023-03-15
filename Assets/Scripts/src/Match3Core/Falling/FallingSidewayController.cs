using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;
using Match3Core;
using Match3Core.RandomGenerate;

namespace Match3Core.Falling
{
    public class FallingSidewayController : FallingController
    {
        public FallingSidewayController(IFallLineModel fallLineModel, SwitchCellsContoller switchCellsContoller)
            : base(fallLineModel, switchCellsContoller)
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
                if (!_fallLineModel.GetCanHoldCell(new Coordinate(x, coordinate.y)) && 
                    !_fallLineModel.GetCanPassCell(new Coordinate(x, coordinate.y)))
                {
                    return false;
                }
            }
            return true;
        }

        private Coordinate CheckSideway(Coordinate coordinate, int vector)
        {
            var newCoordinate = new Coordinate(coordinate.x - 1, coordinate.y + vector);
            if (newCoordinate.x == -1) return newCoordinate;
            if (newCoordinate.y == -1 || newCoordinate.y > _fallLineModel.GetCollumns() - 1
                || (!_fallLineModel.GetCanHoldCell(newCoordinate) &&
                !_fallLineModel.GetCanPassCell(newCoordinate))) return null;
            
            if (_fallLineModel.HasCell(newCoordinate) || CheckWayToZeroCoordinate(newCoordinate) 
                || CheckSideway(newCoordinate, vector) != null) return newCoordinate;
            return null;
        }
    }
}