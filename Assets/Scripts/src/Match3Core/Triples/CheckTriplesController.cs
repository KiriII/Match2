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

        public List<Coordinate> FindTriples()
        { 
            var findedCells = new List<Coordinate>();

            var slots = _boardModel.GetSlots();

            foreach(var s in slots)
            {
                if (s.CanHoldCell && s.Cell == null)
                {
                    findedCells.Add(s.Coordinate);
                }
            }

            OnTriplesFinded(findedCells);
            return findedCells;
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