using System;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;

namespace Match3Core.Board
{
    public class BoardModel : ISwitchCellsModel, 
        IFallLineModel, 
        IGUIBoardModel, 
        ICheckTriplesBoardModel, 
        ITurnModel
    {
        private Slot[,] _board;
        private readonly int _rows;
        private readonly int _columns;

        /*   �� ���� �� ������� ����� ������� ���� � �� ������ ������ ����
        public BoardModel(int x, int y)
        {
            _board = new Slot[x, y];
            _rows = x;
            _columns = y;
        }*/

        public BoardModel(bool[,] canHoldCellBoard)
        {
            _rows = canHoldCellBoard.GetLength(0);
            _columns = canHoldCellBoard.GetLength(1);
            _board = new Slot[_rows, _columns];
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    var coordinate = new Coordinate(i , j);
                    _board[i, j] = canHoldCellBoard[i, j] ? new Slot(coordinate, true) : new Slot(coordinate, false);
                }
            }
        }

        public BoardModel(Slot[,] slots)
        {
            _rows = slots.GetLength(0);
            _columns = slots.GetLength(1);
            _board = new Slot[_rows, _columns];
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    _board[i, j] = new Slot(slots[i,j]);
                }
            }
        }

        public void SetCell(Coordinate coordinate, Cell cell)
        {
            var slot = _board[coordinate.x, coordinate.y];
            if (!slot.CanHoldCell)
            {
                throw new Exception("Try to add Cell to the Blocked Slot");
            }
            slot.Cell = cell;
        }

        public Cell[] GetFullRow(int rowNumber)
        {
            var row = new Cell[_rows];
            for (int i = 0; i < _rows; i++)
            {
                row[i] = GetCanHoldCell(rowNumber, i) ? GetCell(rowNumber, i) : null;
            }
            return row;
        }
        
        public Cell[] GetFullCollumn(int collumnNumber)
        {
            var collumn = new Cell[_columns];
            for (int i = 0; i < _columns; i++)
            {
                collumn[i] = GetCanHoldCell(i, collumnNumber) ? GetCell(i, collumnNumber) : null;
            }
            return collumn;
        }
        
        public Cell GetCell(Coordinate coordinate)
        {
            if (!GetCanHoldCell(coordinate)) throw new Exception("Try to get Cell from Blocked Slot");
            var slot = _board[coordinate.x, coordinate.y];
            return slot.Cell;
        }

        public Cell GetCell(int x, int y)
        {
            return GetCell(new Coordinate(x, y));
        }

        public bool GetCanHoldCell(Coordinate coordinate)
        {
            var slot = _board[coordinate.x, coordinate.y];
            return slot.CanHoldCell;
        }

        public bool GetCanHoldCell(int x, int y)
        {
            return GetCanHoldCell(new Coordinate(x, y));
        }

        public bool GetCanPassCell(Coordinate coordinate)
        {
            var slot = _board[coordinate.x, coordinate.y];
            return slot.CanPassCell;
        }

        public bool GetCanPassCell(int x, int y)
        {
            return GetCanPassCell(new Coordinate(x, y));
        }

        public bool GetBlocked(Coordinate coordinate)
        {
            var slot = _board[coordinate.x, coordinate.y];
            return slot.IsBlocked;
        }

        public bool GetBlocked(int x, int y)
        {
            return GetCanPassCell(new Coordinate(x, y));
        }

        public bool ContainCoordinate(Coordinate coordinate)
        {
            if (coordinate.x >= 0 && coordinate.x < _rows && coordinate.y >= 0 && coordinate.y < _columns) return true;
            return false;
        }

        public int GetRows()
        {
            return _rows;
        }

        public int GetCollumns()
        {
            return _columns;
        }

        public Slot[,] GetSlots()
        {
            return _board;
        }

        public Cell[,] GetCells()
        {
            var cells = new Cell[_rows, _columns];

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    cells[i, j] = _board[i, j].Cell;
                }
            }

            return cells;
        }

        public void ClearBoard()
        {
            foreach (var s in _board)
            {
                s.Cell = null;
            }
        }

        public bool HasCell(Coordinate coordinate)
        {
            var result = (GetCanHoldCell(coordinate) && !GetBlocked(coordinate)) ? GetCell(coordinate).color != CellsColor.Empty : false;
            return result;
        }

        // --- Debug ---
        public void PrintCurrnetBoard()
        {
            var result = "";
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    var cell = _board[i, j].Cell;
                    if (cell == null)
                    {
                        result += String.Format("{0,3}", "[]");
                    }
                    else
                    {
                        result += String.Format("{0,3}", (_board[i, j].CanHoldCell ? (int)cell.color : "X"));
                    }
                }
                result += "\n";
            }
            Debug.Log(result);
        }
    }
}