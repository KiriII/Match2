using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;
using Match3Core.MakeTurn;

namespace Match3Input
{
    public class DestroyCellState : InputState
    {
        private Match3GameCore _gameCore;
        private InputController _inputController;

        public DestroyCellState(InputController inputController, Match3GameCore gameCore)
        {
            state = States.DestroyCell;
            _inputController = inputController;
            _gameCore = gameCore;
            Debug.Log(state);
        }

        public override void MakeTurn(Turn turn)
        {
            if (turn.vector == Vector2.zero)
            {
                Debug.Log($"Destroy {turn.coordinate} {turn.vector}");
                _gameCore.DestroyCell(turn.coordinate);

                _inputController.ChangeState(0);
            }
        }
    }
}