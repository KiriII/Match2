using System;
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

        private event Action<Vector3> _slotDestroyed;

        public DestroySlotState(InputController inputController, Match3GameCore gameCore, Action<Vector3> slotDestroyed)
        {
            state = States.DestroySlot;
            _inputController = inputController;
            _gameCore = gameCore;
            _slotDestroyed = slotDestroyed;
        }

        public override void MakeTurn(Turn turn)
        {
            if (turn.fallenSlot != null)
            {
                _inputController.ChangeState(3);
                _inputController.TurnMade(turn);
            }
            if (turn.vector == Vector2.zero)
            {
                if (_gameCore.DestroySlot(turn.coordinate))
                {
                    _slotDestroyed?.Invoke(turn.position);
                    _inputController.ChangeState(0);
                }
            }
        }
    }
}