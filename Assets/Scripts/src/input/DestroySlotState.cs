using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;
using Match3Core.MakeTurn;

namespace Match3Input
{
    public class DestroySlotState : InputState
    {
        private Match3GameCore _gameCore;
        private InputController _inputController;

        public DestroySlotState(InputController inputController, Match3GameCore gameCore)
        {
            state = States.DestroySlot;
            _inputController = inputController;
            _gameCore = gameCore;
        }

        public override void MakeTurn(Turn turn)
        {
            if (turn.vector == Vector2.zero)
            {
                if (_gameCore.DestroySlot(turn.coordinate))
                {
                    Debug.Log($"Destroy Slot {turn.coordinate} {turn.vector}");
                    _inputController.ChangeState(0);
                }
            }
        }
    }
}