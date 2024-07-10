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

        private event Action<List<Coordinate>> _cellsDestroyed;
        private event Action _updateView;

        public CellsDestroyController(SwitchCellsContoller switchCellsContoller, IBoardCellDestroyModel cellDestroyModel, Action updateView)
        {
            _cellDestroyModel = cellDestroyModel;
            _updateView = updateView;
            _switchCellsContoller = switchCellsContoller;
        }

        // �� ����� ���� �� ���������� � ������������� � ������ ����

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
            _switchCellsContoller.SwitchWithNewCell(coordinate, new Cell());
            OnViewUpdate();
            OnCellsDestroyed(new List<Coordinate> { coordinate });
            return true;
        }

        private void DestroyCells(List<Coordinate> tripledCells, bool forceDestroy = false)
        {
            foreach (var cell in tripledCells)
            {
                if (!(_cellDestroyModel.GetCanDestroyCell(cell) || forceDestroy)) continue;
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
    }
}