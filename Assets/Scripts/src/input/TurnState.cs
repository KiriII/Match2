using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;
using Match3Core.MakeTurn;

namespace Match3Input
{
    public class TurnState : InputState
    {
        private Match3GameCore _gameCore;

        public TurnState(Match3GameCore gameCore)
        {
            state = States.Turn;
            _gameCore = gameCore;
        }
        
        public override void MakeTurn(Turn turn)
        {
            if (turn.vector != Vector2.zero)
            {
                Debug.Log($"TURN {turn.coordinate} {turn.vector}");
                _gameCore.TurnMade(turn);
            }
        }
    }
}
