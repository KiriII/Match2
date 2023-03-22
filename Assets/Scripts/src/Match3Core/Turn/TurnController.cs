using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;

namespace Match3Core.MakeTurn
{
    public class TurnController
    {
        private ITurnModel _turnModel;
        private SwitchCellsContoller _switchCellsContoller;

        private event Action _correctTurnDone;

        public TurnController(ITurnModel turnModel, SwitchCellsContoller switchCellsContoller)
        {
            _turnModel = turnModel;
            _switchCellsContoller = switchCellsContoller;
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

            //Debug.Log($"{firstCoordinate} {secondCoordinate}");

            var cells = CopyArray(_turnModel.GetCells());
            var c = new Cell(cells[firstCoordinate.x, firstCoordinate.y]);
            cells[firstCoordinate.x, firstCoordinate.y] = new Cell(cells[secondCoordinate.x, secondCoordinate.y]);
            cells[secondCoordinate.x, secondCoordinate.y] = new Cell(c);
            var newTriples = FindTriples(cells);
            //Debug.Log(newTriples);

            if (newTriples)
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

        public bool FindTriples(Cell[,] cells)
        {
            var findedCells = new List<Coordinate>();

            var rows = cells.GetLength(0);
            var collumns = cells.GetLength(1);

            if (rows != collumns) throw new Exception($"Board have uncorrect size {rows}x{collumns}");

            for (int i = 0; i < rows; i++)
            {
                var YcoordinatesOfTriples = SameCellsFinder.CheckSameInArray(GetFullRow(cells, i));
                var XcoordinatesOfTriples = SameCellsFinder.CheckSameInArray(GetFullCollumn(cells, i));

                foreach (var y in YcoordinatesOfTriples)
                {
                    if (!FindCoordinateInFinded(findedCells, i, y))
                    {
                        //Debug.Log($"{new Coordinate(i, y)}");
                        findedCells.Add(new Coordinate(i, y));
                    }
                }
                foreach (var x in XcoordinatesOfTriples)
                {
                    if (!FindCoordinateInFinded(findedCells, x, i))
                    {
                        //Debug.Log($"{new Coordinate(x, i)}");
                        findedCells.Add(new Coordinate(x, i));
                    }
                }
            }
            return findedCells.Count > 0;
        }

        // BAD
        private bool FindCoordinateInFinded(List<Coordinate> findedCells, int x, int y)
        {
            var contain = false;
            foreach (var c in findedCells)
            {
                if ((c.x == x) && (c.y == y))
                {
                    contain = true;
                }
            }
            return contain;
        }

        public Cell[] GetFullRow(Cell[,] cells, int rowNumber)
        {
            var row = new Cell[cells.GetLength(0)];
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                row[i] = cells[rowNumber, i];
            }
            return row;
        }

        public Cell[] GetFullCollumn(Cell[,] cells, int collumnNumber)
        {
            var collumn = new Cell[cells.GetLength(1)];
            for (int i = 0; i < cells.GetLength(1); i++)
            {
                collumn[i] = cells[i, collumnNumber];
            }
            return collumn;
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