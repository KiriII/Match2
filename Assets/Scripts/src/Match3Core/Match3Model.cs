using System;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core
{
    public class Match3Model : 
        ICellsFallModel, 
        ICellsAddModel, 
        ITripleCellsCheckModel, 
        ICellsDestroyModel, 
        ISwitchCellsModel
    {
        private readonly Cell[,] _board; 

        private readonly int _rows;      // ?
        private readonly int _columns;   // ?

        public Match3Model(int x, int y)
        {
            _board = new Cell[x, y];
            _rows = x;
            _columns = y;

            ClearBoard();
        }

        public Match3Model(Cell[,] board)
        {
            _rows = board.GetLength(0);
            _columns = board.GetLength(1);
            _board = new Cell[_rows, _columns];
            Array.Copy(board, _board, _rows*_columns);
        }

        private void ClearBoard()
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    _board[i, j] = new Cell(CellsColor.Empty); 
                }
            }
        }

        // ----- Maybe need to delete this ----- 
        public void FallAliveCellsInCollumn(int collumn, int lastDestroyedCell, int destroyedCellsCount = 1)
        {
            for (int i = 1; i <= destroyedCellsCount; i++)
            {
                for (int j = lastDestroyedCell; j > 0; j--)
                {
                    _board[j, collumn] = _board[j - 1, collumn];
                }
            }
        }
        public void DestroyCell(int positionX, int positionY)
        {
            _board[positionY, positionX].color = CellsColor.Empty;
        }

        public void RaiseDeadCell(int positionX, int positionY, int roof = 0)
        {
            if (_board[positionY, positionX].color != CellsColor.Empty)
                Debug.LogWarning($"Try to rais not dead cell in {positionY}, {positionX}");
            for (int i = positionY; i > roof; i--)
            {
                SwitchTwoCells((positionX, i) , (positionX, i - 1));
            }
        }


        public void AddNewBunchOffCellsToCollumn(int collumn, List<Cell> newCells)
        {
            for(int i = 0; i < newCells.Count; i++)
            {
                _board[i, collumn] = new Cell(newCells[i]);
            }
        }

        public void AddNewCellToCollumn(int collumn, Cell newCell)
        {
            _board[0, collumn] = new Cell(newCell);
        }

        public void SetCellColorInPosition(int collumn, int row, CellsColor color)
        {
            _board[row, collumn].color = color;
        }

        public void SetCellInPosition(int collumn, int row, Cell cell)
        {
            _board[row, collumn] = cell;
        }

        public void SwitchTwoCells((int x, int y) switchCell1, (int x, int y) switchCell2)
        {
            var deadCell = _board[switchCell1.y, switchCell1.x];
            _board[switchCell1.y, switchCell1.x] = _board[switchCell2.y , switchCell2.x];
            _board[switchCell2.y, switchCell2.x] = deadCell;
        }

        // ----- Getters -----

        public int GetColumnsCount()
        {
            return _columns;
        }

        public int GetRowsCount()
        {
            return _rows;
        }

        public Cell GetCell(int x, int y)
        {
            return _board[x, y];
        }

        public Cell[] GetCollumnArray(int collumnNumber)
        {
            var collumn = new Cell[_rows];

            for (int i = 0; i < _rows; i++)
            {
                collumn[i] = _board[i, collumnNumber];
            }

            return collumn;
        }

        public Cell[] GetRowArray(int rowNumber)
        {
            var row = new Cell[_columns];

            for (int i = 0; i < _columns; i++)
            {
                row[i] = _board[rowNumber, i];
            }

            return row;
        }

        public Cell[,] GetBoard()
        {
            return _board;
        }

        public string Print()           
        {
            var result = ""; 
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                    result += String.Format("{0,3}", (_board[i, j] is null ? -1 : (int)_board[i, j].color));
                result += "\n";
            }
            return result;
        }
    }
}
