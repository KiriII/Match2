using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3.Board;
using Match3Core;

namespace Match3.Falling
{
    public abstract class StealingCellsController
    {
        public abstract void TrainOfSteals(IFallLineModel fallLineModel, Coordinate coordinate);
    }
}