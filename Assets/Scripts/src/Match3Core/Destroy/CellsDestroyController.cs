using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Match3Core.Board;

namespace Match3Core.DestroyCells
{
    public class CellsDestroyController
    {
        private SwitchCellsContoller _switchCellsContoller;
        private ICellDestroyModel _cellDestroyModel;

        private event Action<List<Coordinate>> _cellsDestroyed;
        private event Action _updateView;

        public CellsDestroyController(SwitchCellsContoller switchCellsContoller, ICellDestroyModel cellDestroyModel, Action updateView)
        {
            _cellDestroyModel = cellDestroyModel;
            _updateView = updateView;
            _switchCellsContoller = switchCellsContoller;
        }

        // На самом деле не уничтожаем а перекрашиваем в пустой цвет

        public bool DestroyCells(Coordinate coordinate)
        {
            if (!_cellDestroyModel.GetCanDestroyCell(coordinate)) return false;
            Debug.Log(_cellDestroyModel.GetCanDestroyCell(coordinate));
            _switchCellsContoller.SwitchWithNewCell(coordinate, new Cell());
            OnViewUpdate();
            OnCellsDestroyed(new List<Coordinate> { coordinate });
            return true;
        }

        public void DestroyCells(List<Coordinate> tripledCells)
        {
            foreach (var cell in tripledCells)
            {
                if (!_cellDestroyModel.GetCanDestroyCell(cell)) continue;
                Debug.Log(cell);
                _switchCellsContoller.SwitchWithNewCell(cell, new Cell());
            }
            //Debug.Log(String.Join(" ", tripledCells));
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
    }
}