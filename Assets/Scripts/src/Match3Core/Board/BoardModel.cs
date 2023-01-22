using System;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;

namespace Match3.Board
{
    public class BoardModel : ISwitchCellsModel
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

        public void SetCell(Cell cell, int x, int y)
        {
            var slot = _board[x, y];
            if (slot.CanHoldCell)
            {
                slot.Cell = cell;
            }
            else
            {
                throw new Exception("Try to add Cell to the Blocked Slot");
            }
        }

        public Cell GetCell(int x, int y)
        {
            var slot = _board[x, y];
            return slot.Cell;
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