using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;

namespace Match3Core.Triples
{
    public class CheckTriplesController
    {
        private IBoardCheckTriplesModel _boardModel;

        private event Action<List<Coordinate>> _triplesFinded;

        public CheckTriplesController(IBoardCheckTriplesModel boardModel)
        {
            _boardModel = boardModel;
        }

        public void FindTriples()
        {
            var cells = _boardModel.GetCells();

            var findedCells = SameCellsFinder.TriplesFinder(cells);

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