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
        public DestroySlotState(InputController controller, InputStateMachine stateMachine) : base(controller, stateMachine)
        {
        }

        public override void MakeTurn(Turn turn)
        {
            Debug.Log("DESTROY SLOT");
            base.MakeTurn(turn);
            if (turn.vector == Vector2.zero)
            {
                if (controller.DestroySlot(turn.coordinate, turn.position))
                {
                    stateMachine.ChangeState(controller.turnState);
                }
            }
        }
    }
}