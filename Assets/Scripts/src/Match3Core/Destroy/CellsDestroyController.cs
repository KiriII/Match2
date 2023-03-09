using Match3Core.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Match3Core.DestroyCells
{
    public class CellsDestroyController
    {
        private SwitchCellsContoller _switchCellsContoller;
        private event Action<List<Coordinate>> _cellsDestroyed;

        public CellsDestroyController(SwitchCellsContoller switchCellsContoller)
        {
            _switchCellsContoller = switchCellsContoller;
        }

        // �� ����� ���� �� ���������� � ������������� � ������ ����
        public void DestroyCells(List<Coordinate> tripledCells)
        {
            SortDestroyedCells(ref tripledCells);
            foreach (var cell in tripledCells)
            {
                _switchCellsContoller.SwitchWithNewCell(cell, new Cell());
            }

            OnCellsDestroyed(tripledCells);
        }

        private void SortDestroyedCells(ref List<Coordinate> destroyedCells)
        {
            destroyedCells.Sort();
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