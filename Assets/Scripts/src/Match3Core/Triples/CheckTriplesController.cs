using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;

namespace Match3Core
{
    public class CheckTriplesController
    {
        private IBoardCheckTriplesModel _boardModel;

        private event Action<List<Coordinate>> _triplesFinded;
        private event Action<int> _triplesCount;

        public CheckTriplesController(IBoardCheckTriplesModel boardModel)
        {
            _boardModel = boardModel;
        }

        public void FindTriples()
        {
            var cells = _boardModel.GetCells();

            var findedCells = SameCellsFinder.TriplesFinder(cells, _triplesCount);

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

        private void OnTriplesCount(int triplesCount)
        {
            _triplesCount?.Invoke(triplesCount);
        }

        public void EnableTriplesCountListener(Action<int> methodInLitener)
        {
            _triplesCount += methodInLitener;
        }

        public void DesableTriplesCountListener(Action<int> methodInLitener)
        {
            _triplesCount -= methodInLitener;
        }
    }
}