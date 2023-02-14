using Match3.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Match3Core.DestroyCells
{
    public class CellsDestroyController
    {
        private SwitchCellsContoller _switchCellsContoller;
        private event Action<List<Coordinate>> cellsDestroyed;

        public CellsDestroyController(SwitchCellsContoller switchCellsContoller)
        {
            _switchCellsContoller = switchCellsContoller;
        }

        // На самом деле не уничтожаем а перекрашиваем в пустой цвет
        public void DestroyCells(List<Coordinate> tripledCells)
        {
            SortDestroyedCells(ref tripledCells);
            foreach (var cell in tripledCells)
            {
                Debug.Log(cell.x);
                _switchCellsContoller.SwitchWithNewCell(cell, new Cell());
            }
        }

        private void SortDestroyedCells(ref List<Coordinate> destroyedCells)
        {
            destroyedCells.Sort();
        }
    }
}