using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Match3Core
{
    public class AddCellsController
    {
        private ICellsAddModel _cellsAddModel;
        private System.Random rnd = new System.Random();

        public AddCellsController(ICellsAddModel cellsAddModel)
        {
            _cellsAddModel = cellsAddModel;
        }

        public void FillBoardWithNewCells()
        {
            var collumnCount = _cellsAddModel.GetColumnsCount();
            var rowCount = _cellsAddModel.GetRowsCount();

            for (int collumn = 0; collumn < collumnCount; collumn++)
            {
                for (int row = 0; row < rowCount; row++)
                {
                    AddUniqCell(collumn, row);
                }
            }
        }

        public void AddNewCells(int collumn, int newCellsCount)
        {
            var newCells = GenerateNewCells(newCellsCount);
            _cellsAddModel.AddNewBunchOffCellsToCollumn(collumn, newCells);
        }

        public void AddNewCells((int collumn, int row) cellPosition)
        {
            var newCell = GenerateOneNewCell();
            _cellsAddModel.AddNewCellToCollumn(cellPosition.collumn, newCell);
            Debug.Log(_cellsAddModel.Print());
        }

        public void AddUniqCell(int collumn, int row)
        {
            var previousInCollumnCellColor = CheckPreviousCellInCollumn(collumn, row);
            var previousInRowCellColor = CheckPreviousCellInRow(collumn, row);
            if (previousInCollumnCellColor != CellsColor.Empty || previousInRowCellColor != CellsColor.Empty)
            {
                var NotRepetitiveColors = Enum.GetValues(typeof(CellsColor)).OfType<CellsColor>().ToList()
                    .Where(cell => cell != CellsColor.Empty && cell != previousInCollumnCellColor && cell != previousInRowCellColor).ToList();
                var newCell = NotRepetitiveColors[rnd.Next(0, NotRepetitiveColors.Count)];
                _cellsAddModel.SetCellColorInPosition(collumn, row, newCell);
                return;
            }
            var newCells = GenerateOneNewCell();
            _cellsAddModel.SetCellInPosition(collumn, row, newCells);
        }

        // ----- TODO make input array with previous cells -----
        private CellsColor CheckPreviousCellInCollumn(int collumn, int row)
        {
            if (row <= 1) return CellsColor.Empty;
            var previousCell = _cellsAddModel.GetCell(row - 1, collumn);
            var previousPreviousCell = _cellsAddModel.GetCell(row - 2, collumn);
            if (!previousPreviousCell.Equals(previousCell)) return CellsColor.Empty;
            return previousCell.color;
        }

        private CellsColor CheckPreviousCellInRow(int collumn, int row)
        {
            if (collumn <= 1) return CellsColor.Empty;
            var previousCell = _cellsAddModel.GetCell(row, collumn - 1);
            var previousPreviousCell = _cellsAddModel.GetCell(row, collumn - 2);
            if (!previousPreviousCell.Equals(previousCell)) return CellsColor.Empty;
            return previousCell.color;
        }

        // ----- TODO -----
        private List<Cell> GenerateNewCells(int newCellsCount)
        {
            var result = new List<Cell>();
            for (int i = 0; i < newCellsCount; i++)
            {
                var number = rnd.Next(1, 4);
                result.Add(new Cell((CellsColor)number));
            }
            return result;
        }

        private Cell GenerateOneNewCell()
        {
            var number = rnd.Next(1, 4);
            return new Cell((CellsColor)number);
        }
    }
}