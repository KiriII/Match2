using System;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;

namespace Match3Core.Board
{
    public class BoardModel : ISwitchCellsModel, IFallLineModel, IGUIBoardModel, ICheckTriplesBoardModel
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

        public Cell GetCell(Coordinate coordinate)
        {
            if (!GetCanHoldCell(coordinate)) throw new Exception("Try to get Cell from Blocked Slot");
            var slot = _board[coordinate.x, coordinate.y];
            return slot.Cell;
        }

        public Cell GetCell(int x, int y)
        {
            if (!GetCanHoldCell(x, y)) throw new Exception("Try to get Cell from Blocked Slot");
            var slot = _board[x, y];
            return slot.Cell;
        }

        public bool GetCanHoldCell(Coordinate coordinate)
        {
            var slot = _board[coordinate.x, coordinate.y];
            return slot.CanHoldCell;
        }

        public bool GetCanHoldCell(int x, int y)
        {
            var slot = _board[x, y];
            return slot.CanHoldCell;
        }

        public bool GetCanPassCell(Coordinate coordinate)
        {
            var slot = _board[coordinate.x, coordinate.y];
            return slot.CanPassCell;
        }

        public bool GetCanPassCell(int x, int y)
        {
            var slot = _board[x, y];
            return slot.CanPassCell;
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

        public void ClearBoard()
        {
            foreach (var s in _board)
            {
                s.Cell = null;
            }
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