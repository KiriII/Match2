using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.MakeTurn;

namespace Match3Input
{
    public abstract class InputState
    {
        public States state;

        public abstract void MakeTurn(Turn turn);
    }
}