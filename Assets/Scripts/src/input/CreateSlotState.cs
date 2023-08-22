using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;
using Match3Core.MakeTurn;

namespace Match3Input
{
    public class CreateSlotState : InputState
    {
        private Match3GameCore _gameCore;
        private InputController _inputController;

        public CreateSlotState(InputController inputController, Match3GameCore gameCore)
        {
            state = States.CreateSlot;
            _inputController = inputController;
            _gameCore = gameCore;
        }

        public override void MakeTurn(Turn turn)
        {
            if (turn.vector == Vector2.zero)
            {
                if (_gameCore.CreateSlot(turn.coordinate))
                {
                    Debug.Log($"Create Slot {turn.coordinate} {turn.vector}");
                    _inputController.ChangeState(0);
                }
            }
        }
    }
}