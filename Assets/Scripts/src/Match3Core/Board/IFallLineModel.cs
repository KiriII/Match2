using System;
using System.Collections.Generic;
using Match3Core;

namespace Match3Core.Board
{
    public interface IFallLineModel
    {
        public bool GetCanHoldCell(Coordinate coordinate);
        public bool GetCanPassCell(Coordinate coordinate);
        public bool GetBlocked(Coordinate coordinate);
        public int GetCollumns();
        public bool HasCell(Coordinate coordinate);
    }
}
