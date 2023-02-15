using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;
using Match3Core;

namespace Match3Core.Falling
{
    public abstract class StealingCellsController
    {
        public abstract void TrainOfSteals(Coordinate coordinate);
    }
}