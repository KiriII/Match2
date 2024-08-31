using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Match3Core.Board;

namespace Match3Core
{
    public class CellsDestroyController
    {
        private SwitchCellsContoller _switchCellsContoller;
        private IBoardCellDestroyModel _cellDestroyModel;

        private event Action<IEnumerable<Coordinate>> _cellsFinded;
        private event Action<List<Coordinate>> _cellsDestroyed;
        private event Action _updateView;

        public CellsDestroyController(SwitchCellsContoller switchCellsContoller, IBoardCellDestroyModel cellDestroyModel, Action updateView)
        {
            _cellDestroyModel = cellDestroyModel;
            _updateView = updateView;
            _switchCellsContoller = switchCellsContoller;
        }

        // На самом деле не уничтожаем а перекрашиваем в пустой цвет

        public void ForceDestroyCells(List<Coordinate> tripledCells)
        {
            DestroyCells(tripledCells, true);
        }

        public void SimpleDestroyCells(List<Coordinate> tripledCells)
        {
            DestroyCells(tripledCells, false);
        }

        public bool DestroyCells(Coordinate coordinate, bool forceDestroy = false)
        {
            if (!(_cellDestroyModel.GetCanDestroyCell(coordinate) || forceDestroy)) return false;
            OnCellsFinded(new List<Coordinate> { coordinate });
            _switchCellsContoller.SwitchWithNewCell(coordinate, new Cell());
            OnViewUpdate();
            OnCellsDestroyed(new List<Coordinate> { coordinate });
            return true;
        }

        private void DestroyCells(List<Coordinate> tripledCells, bool forceDestroy = false)
        {
            HashSet<Coordinate> canDestroyCells = new HashSet<Coordinate>();
            foreach (var cell in tripledCells)
            {
                if (!(_cellDestroyModel.GetCanDestroyCell(cell) || forceDestroy)) continue;
                canDestroyCells.Add(cell);
            }
            OnCellsFinded(canDestroyCells);

            foreach (var cell in canDestroyCells)
            {
                _switchCellsContoller.SwitchWithNewCell(cell, new Cell());
            }
            OnViewUpdate();
            OnCellsDestroyed(tripledCells);
        }

        private void OnViewUpdate()
        {
            _updateView?.Invoke();
        }

        private void OnCellsDestroyed(List<Coordinate> destroyedCells)
        {
            _cellsDestroyed?.Invoke(destroyedCells);
        }

        public void EnableCellDestroyedListener(Action<List<Coordinate>> methodInLitener)
        {
            _cellsDestroyed += methodInLitener;
        }

        public void DesableCellDestroyedListener(Action<List<Coordinate>> methodInLitener)
        {
            _cellsDestroyed -= methodInLitener;
        }

        private void OnCellsFinded(IEnumerable<Coordinate> destroyedCells)
        {
            _cellsFinded?.Invoke(destroyedCells);
        }

        public void EnableCellsFindedListener(Action<IEnumerable<Coordinate>> methodInLitener)
        {
            _cellsFinded += methodInLitener;
        }

        public void DesableCellsFindedListener(Action<IEnumerable<Coordinate>> methodInLitener)
        {
            _cellsFinded -= methodInLitener;
        }
    }
}