using System;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;

namespace Match3Core.Board
{
    public class BoardModel : IBoardSwitchCellsModel, 
        IBoardFallLineModel, 
        IBoardCheckTriplesModel,
        IBoardTurnModel,
        IBoardSlotUnblockModel,
        IBoardSwitchSlotModel,
        IBoardCellDestroyModel,
        IBoardSlotManipulateModel,
        IBoardBoxModel,
        IBoardShockerModel
    {
        private Slot[,] _board;
        private readonly int _rows;
        private readonly int _columns;

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

        public void UnblockSlot(Coordinate coordinate)
        {
            _board[coordinate.x, coordinate.y].IsBlocked = false;
        }

        public bool HasCell(Coordinate coordinate)
        {
            var result = (GetCanHoldCell(coordinate) && !GetBlocked(coordinate)) ? GetCell(coordinate).Color != CellsColor.Empty : false;
            return result;
        }

        public bool ContainCoordinate(Coordinate coordinate)
        {
            if (coordinate.x >= 0 && coordinate.x < _rows && coordinate.y >= 0 && coordinate.y < _columns) return true;
            return false;
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

        public void SetSlot(Coordinate coordinate, Slot slot)
        {
            _board[coordinate.x, coordinate.y] = slot;
        }

        #region Getters
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

        public List<Coordinate> GetNeighbourSlot(Coordinate coordinate)
        {
            var neighbours = new List<Coordinate>();
            if (ContainCoordinate(new Coordinate(coordinate, Vector2.up))) neighbours.Add(new Coordinate(coordinate, Vector2.up));
            if (ContainCoordinate(new Coordinate(coordinate, Vector2.down))) neighbours.Add(new Coordinate(coordinate, Vector2.down));
            if (ContainCoordinate(new Coordinate(coordinate, Vector2.right))) neighbours.Add(new Coordinate(coordinate, Vector2.right));
            if (ContainCoordinate(new Coordinate(coordinate, Vector2.left))) neighbours.Add(new Coordinate(coordinate, Vector2.left));
            return neighbours;
        }

        public Slot GetSlot(Coordinate coordinate)
        {
            return _board[coordinate.x, coordinate.y];
        }

        public Slot GetSlot(int x, int y)
        {
            return GetSlot(new Coordinate(x, y));
        }

        public Cell GetCell(Coordinate coordinate)
        {
            if (!GetCanHoldCell(coordinate)) throw new Exception($"Try to get Cell from Blocked Slot {coordinate}");
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
            return slot.CanHoldCell && slot.IsActive && !GetBlocked(coordinate);
        }

        public bool GetCanHoldCell(int x, int y)
        {
            return GetCanHoldCell(new Coordinate(x, y));
        }

        public bool GetCanDestroyCell(Coordinate coordinate)
        {
            var cell = _board[coordinate.x, coordinate.y].Cell;
            return !(cell.Color == CellsColor.Empty) && 
                !(cell.Color == CellsColor.Special) &&
                GetCanHoldCell(coordinate);
        }

        public bool GetCanDestroyCell(int x, int y)
        {
            return GetCanDestroyCell(new Coordinate(x, y));
        }

        public bool GetCanDestroySlot(Coordinate coordinate)
        {
            if (!GetCanHoldCell(coordinate)) return false;
            var cell = _board[coordinate.x, coordinate.y].Cell;
            return !(cell.Color == CellsColor.Special);
        }

        public bool GetCanDestroySlot(int x, int y)
        {
            return GetCanDestroySlot(new Coordinate(x, y));
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
            return GetBlocked(new Coordinate(x, y));
        }

        public bool GetActive(Coordinate coordinate)
        {
            var slot = _board[coordinate.x, coordinate.y];
            return slot.IsActive;
        }

        public bool GetActive(int x, int y)
        {
            return GetActive(new Coordinate(x, y));
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

        public Slot[,] GetBoardCopy()
        {
            var copy = new Slot[_rows, _columns];
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    copy[i, j] = new Slot(_board[i, j]);
                }
            }
            return copy;
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

        public List<Coordinate> GetCoordinateSlotsWithCellsInRow(Coordinate coordinate)
        {
            Debug.Log(coordinate);
            int rowIndex = coordinate.x;
            var slotsWithCells = new List<Coordinate>();
            for (int i = 0; i < _rows; i++)
            {
                if (GetCanHoldCell(rowIndex, i))
                {
                    slotsWithCells.Add(new Coordinate(rowIndex, i));
                }
            }
            return slotsWithCells;
        }

        public List<Coordinate> GetCoordinateSlotsWithCellsInCollumn(Coordinate coordinate)
        {
            Debug.Log(coordinate);
            int collumnIndex = coordinate.y;
            var slotsWithCells = new List<Coordinate>();
            for (int i = 0; i < _columns; i++)
            {
                if (GetCanHoldCell(i, collumnIndex))
                {
                    slotsWithCells.Add(new Coordinate(i, collumnIndex));
                }
            }
            return slotsWithCells;
        }

        public List<Coordinate> GetEmptyCoordinates()
        {
            return EmptyCellsFinder.FindEmpty(_board);
        }
        #endregion

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
                        result += String.Format("{0,3}", (_board[i, j].CanHoldCell ? (int)cell.Color : "X"));
                    }
                }
                result += "\n";
            }
            Debug.Log(result);
        }
    }
}