using UnityEngine;
using Match3Core.MakeTurn;

namespace Match3Input
{
    public class TurnState : InputState
    {
        public TurnState(InputController controller, InputStateMachine stateMachine) : base(controller, stateMachine)
        {
        }
        
        public override void MakeTurn(Turn turn)
        {
            Debug.Log("TURN");
            base.MakeTurn(turn);
            if (turn.vector != Vector2.zero)
            {
                Debug.Log($"TURN {turn}");
                controller.MakeSwapTurn(turn);
            }
        }
    }
}
