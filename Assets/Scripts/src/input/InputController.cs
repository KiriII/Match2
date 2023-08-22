using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;
using Match3Core.MakeTurn;

namespace Match3Input
{
    public class InputController
    {
        public InputState State { get; set; }

        private Match3GameCore _gameCore;

        public InputController(Match3GameCore gameCore)
        {
            _gameCore = gameCore;

            this.State = new TurnState(_gameCore);
        }

        public void ChangeState(int state)
        {
            if ((int)State.state == state) return;
            switch (state)
            {
                case 0:
                    State = new TurnState(_gameCore);
                    break;
                case 1:
                    State = new DestroyCellState(this, _gameCore);
                    break;
                case 2:
                    State = new DestroySlotState(this, _gameCore);
                    break;
                case 3:
                    State = new CreateSlotState(this, _gameCore);
                    break;
                default:
                    break;
            }
        }

        public void TurnMade(Turn turn)
        {
            State.MakeTurn(turn);
        }
    }
}
