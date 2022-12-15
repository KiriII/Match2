using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Src.Match3Core
{
    public class FindDestroyedCellsController
    {
        private event Action<(int,int)> _detroyedCellFind;
        private event Action<(int, int)> _detroyedCellDestroyed;


        // ------ TODO Надо переделать так, чтобы все столбцы падали паралелльно -----
        public void FindDestroyedCellsInCollumns(List<(int x, int y)> destroyedCells, int rowsCount)
        {
            for (int cell = 0; cell < destroyedCells.Count; cell++)
            {
                OnDestroyedCellFind((destroyedCells[cell].y, destroyedCells[cell].x));
            }

            for (int row = 0; row < rowsCount; row++)
            {
                var _deadCellsInCollumn = new List<(int x,int y)>(destroyedCells
                    .Where(cell => cell.x == row)
                    .OrderBy(i => i));

                foreach (var (x, y) in _deadCellsInCollumn)
                {
                    OnDestroyedCellDestroyed((y, x));
                }
            }
        }

        private void OnDestroyedCellFind((int x, int y) cellPosition)
        {
            _detroyedCellFind?.Invoke(cellPosition);
        }

        public void EnableDestroyedCellFindListener(Action<(int, int)> methodInLitener)
        {
            _detroyedCellFind += methodInLitener;
        }

        public void DesableDestroyedCellFindListener(Action<(int, int)> methodInLitener)
        {
            _detroyedCellFind -= methodInLitener;
        }

        private void OnDestroyedCellDestroyed((int x, int y) cellPosition)
        {
            _detroyedCellDestroyed?.Invoke(cellPosition);
        }

        public void EnableDestroyedCellDestroyedListener(Action<(int, int)> methodInLitener)
        {
            _detroyedCellDestroyed += methodInLitener;
        }

        public void DesableDestroyedCellDestroyedListener(Action<(int, int)> methodInLitener)
        {
            _detroyedCellDestroyed -= methodInLitener;
        }
    }
}
