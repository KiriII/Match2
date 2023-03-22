using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;

namespace Match3Core.Triples
{
    public class CheckTriplesController
    {
        private ICheckTriplesBoardModel _boardModel;

        private event Action<List<Coordinate>> _triplesFinded;

        public CheckTriplesController(ICheckTriplesBoardModel boardModel)
        {
            _boardModel = boardModel;
        }

        public void FindTriples()
        {
            var findedCells = new List<Coordinate>();

            var rows = _boardModel.GetRows();
            var collumns = _boardModel.GetCollumns();

            if (rows != collumns) throw new Exception($"Board have uncorrect size {rows}x{collumns}"); 

            for (int i = 0; i < rows; i++)
            {
                var YcoordinatesOfTriples = SameCellsFinder.CheckSameInArray(_boardModel.GetFullRow(i)) ;
                var XcoordinatesOfTriples = SameCellsFinder.CheckSameInArray(_boardModel.GetFullCollumn(i));

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
            OnTriplesFinded(findedCells);
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

        private void OnTriplesFinded(List<Coordinate> findedCells)
        {
            _triplesFinded?.Invoke(findedCells);
        }

        public void EnableTriplesFindedListener(Action<List<Coordinate>> methodInLitener)
        {
            _triplesFinded += methodInLitener;
        }

        public void DesableTriplesFindedListener(Action<List<Coordinate>> methodInLitener)
        {
            _triplesFinded -= methodInLitener;
        }
    }
}