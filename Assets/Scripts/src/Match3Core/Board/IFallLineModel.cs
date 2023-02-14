using System;
using System.Collections.Generic;
using Match3Core;

namespace Match3.Board
{
    public interface IFallLineModel
    {
        public bool GetCanHoldCell(Coordinate coordinate);
        public bool GetCanPassCell(Coordinate coordinate);
    }
}
