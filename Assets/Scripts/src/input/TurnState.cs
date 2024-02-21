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
        private InputController _inputController;

        public TurnState(InputController inputController, Match3GameCore gameCore)
        {
            state = States.Turn;
            _inputController = inputController;
            _gameCore = gameCore;
        }
        
        public override void MakeTurn(Turn turn)
        {
            if (turn.fallenSlot != null)
            {
                _inputController.ChangeState(3);
                _inputController.TurnMade(turn);
            }
            if (turn.vector != Vector2.zero)
            {
                Debug.Log($"TURN {turn}");
                _gameCore.TurnMade(turn);
            }
        }
    }
}
