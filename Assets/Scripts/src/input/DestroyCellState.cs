using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;
using Match3Core.MakeTurn;

namespace Match3Input
{
    public class DestroyCellState : InputState
    {
        public DestroyCellState(InputController controller, InputStateMachine stateMachine) : base(controller, stateMachine)
        {
        }

        public override void MakeTurn(Turn turn)
        {
            Debug.Log("DESTROY CELL");
            base.MakeTurn(turn);
            if (turn.vector == Vector2.zero)
            {
                if (controller.DestroyCell(turn.coordinate))
                {
                    Debug.Log($"Destroy Cell {turn.coordinate} {turn.vector}");
                    stateMachine.ChangeState(controller.turnState);
                }
            }
        }
    }
}