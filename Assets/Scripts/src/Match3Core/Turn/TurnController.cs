using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;

namespace Match3Core.MakeTurn
{
    public class TurnController
    {
        private ITurnModel _turnModel;

        public TurnController(ITurnModel turnModel)
        {
            _turnModel = turnModel;
        }

        public void MakeTurn(Turn turn)
        {
            //Debug.Log($"{turn.coordinate} {turn.vector}");
            var firstCoordinate = turn.coordinate;
            var secondCoordinate = new Coordinate(firstCoordinate, turn.vector);

            if (firstCoordinate.Equals(secondCoordinate)
                || !_turnModel.ContainCoordinate(firstCoordinate)
                || !_turnModel.ContainCoordinate(secondCoordinate)
                || !_turnModel.GetCanHoldCell(firstCoordinate)
                || !_turnModel.GetCanHoldCell(secondCoordinate)
                || _turnModel.GetCell(firstCoordinate).color == CellsColor.Empty
                || _turnModel.GetCell(secondCoordinate).color == CellsColor.Empty) return;

            Debug.Log($"{firstCoordinate} {secondCoordinate}");

            var cells = _turnModel.GetCells().toArray();
            var c = new Cell(cells[firstCoordinate.x, firstCoordinate.y]);
            cells[firstCoordinate.x, firstCoordinate.y] = new Cell(cells[secondCoordinate.x, secondCoordinate.y]);
            cells[secondCoordinate.x, secondCoordinate.y] = new Cell(c);
            var newTriples = SameCellsFinder.CheckSameInArray(cells);
        }
    }
}