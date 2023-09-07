using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;
using Match3Core.Triples;

namespace Match3Core.MakeTurn
{
    public class TurnController
    {
        private IBoardTurnModel _turnModel;
        private SwitchCellsContoller _switchCellsContoller;

        private event Action _correctTurnDone;

        public TurnController(IBoardTurnModel turnModel, SwitchCellsContoller switchCellsContoller)
        {
            _turnModel = turnModel;
            _switchCellsContoller = switchCellsContoller;
        }

        public void MakeTurn(Turn turn)
        {
            var firstCoordinate = turn.coordinate;
            var secondCoordinate = new Coordinate(firstCoordinate, turn.vector);

            if (firstCoordinate.Equals(secondCoordinate)
                || !_turnModel.ContainCoordinate(firstCoordinate)
                || !_turnModel.ContainCoordinate(secondCoordinate)
                || !_turnModel.GetCanHoldCell(firstCoordinate)
                || !_turnModel.GetCanHoldCell(secondCoordinate)
                || _turnModel.GetCell(firstCoordinate).Color == CellsColor.Empty
                || _turnModel.GetCell(secondCoordinate).Color == CellsColor.Empty) return;

            var cells = CopyArray(_turnModel.GetCells());
            var c = new Cell(cells[firstCoordinate.x, firstCoordinate.y]);
            cells[firstCoordinate.x, firstCoordinate.y] = new Cell(cells[secondCoordinate.x, secondCoordinate.y]);
            cells[secondCoordinate.x, secondCoordinate.y] = new Cell(c);
            //var newTriples = SameCellsFinder.TriplesFinder(cells);

            if (SameCellsFinder.TriplesFinder(cells).Count > 0)
            {
                _switchCellsContoller.SwitchCells(firstCoordinate, secondCoordinate);
                OnCorrectTurnDone();
            }
        }

        private Cell[,] CopyArray(Cell[,] source)
        {
            var receiver = new Cell[source.GetLength(0), source.GetLength(1)];
            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    receiver[i, j] = source[i, j];
                }
            }
            return receiver;
        }

        private void OnCorrectTurnDone()
        {
            _correctTurnDone?.Invoke();
        }

        public void EnableCorrectTurnDoneListener(Action methodInLitener)
        {
            _correctTurnDone += methodInLitener;
        }

        public void DesableCSorrectTurnDoneListener(Action methodInLitener)
        {
            _correctTurnDone -= methodInLitener;
        }
    }
}