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

            for (int i = 0; i < rows; i++)
            {
                var YcoordinatesOfTriples = SameCellsFinder.CheckSameInArray(_boardModel.GetFullRow(i)) ;

                foreach (var y in YcoordinatesOfTriples)
                {
                    Debug.Log($"{new Coordinate(i, y)}");
                    findedCells.Add(new Coordinate(i, y));
                }
            }

            for (int i = 0; i < collumns; i++)
            {
                var XcoordinatesOfTriples = SameCellsFinder.CheckSameInArray(_boardModel.GetFullCollumn(i));

                foreach (var x in XcoordinatesOfTriples)
                {
                    Debug.Log($"{new Coordinate(x, i)}");
                    findedCells.Add(new Coordinate(x, i));
                }
            }

            OnTriplesFinded(findedCells);
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