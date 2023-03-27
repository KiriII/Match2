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
        private event Action<List<Coordinate>> _cellsDestroyed;

        public CellsDestroyController(SwitchCellsContoller switchCellsContoller)
        {
            _switchCellsContoller = switchCellsContoller;
        }

        // На самом деле не уничтожаем а перекрашиваем в пустой цвет

        public void DestroyCells(Coordinate coordinate)
        {
            _switchCellsContoller.SwitchWithNewCell(coordinate, new Cell());
            OnCellsDestroyed(new List<Coordinate> { coordinate });
        }

        public void DestroyCells(List<Coordinate> tripledCells)
        {
            foreach (var cell in tripledCells)
            {
                _switchCellsContoller.SwitchWithNewCell(cell, new Cell());
            }

            OnCellsDestroyed(tripledCells);
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