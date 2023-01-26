using System;
using System.Collections.Generic;
using Match3Core;

namespace Match3.Board
{
    public interface IFallLineModel
    {
        public void SetSomeCells(List<Coordinate> coordinate, List<Cell> cells);

        public List<Cell> GetSomeCells(List<Coordinate> coordinates);

        public bool GetCanHoldCell(Coordinate coordinate);

        public int GetCollumns();

        public int GetRows();
    }
}
