using System;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;

namespace Match3.Board
{
    public class BoardModel : ISwitchCellsModel, IFallLineModel
    {
        private Slot[,] _board;
        private readonly int _rows;
        private readonly int _columns;

        /*   По идее мы передаём сразу готовое поле и не меняем размер поля
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
                    _board[i, j] = canHoldCellBoard[i, j] ? new Slot(true) : new Slot(false);
                }
            }
        }

        public void SetCell(Coordinate coordinate, Cell cell)
        {
            var slot = _board[coordinate.x, coordinate.y];
            if (slot.CanHoldCell)
            {
                slot.Cell = cell;
            }
            else
            {
                throw new Exception("Try to add Cell to the Blocked Slot");
            }
        }

        public Cell GetCell(Coordinate coordinate)
        {
            if (!GetCanHoldCell(coordinate)) throw new Exception("Try to get Cell from Blocked Slot");
            var slot = _board[coordinate.x, coordinate.y];
            return slot.Cell;
        }

        public bool GetCanHoldCell(Coordinate coordinate)
        {
            var slot = _board[coordinate.x, coordinate.y];
            return slot.CanHoldCell;
        }

        public int GetRows()
        {
            return _rows;
        }

        public int GetCollumns()
        {
            return _columns;
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
                    result += String.Format("{0,3}", (_board[i, j].CanHoldCell ? (int)cell.color : "X"));
                }
                result += "\n";
            }
            Debug.Log(result);
        }
    }
}