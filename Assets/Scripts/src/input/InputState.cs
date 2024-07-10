using System;
using UnityEngine;
using Match3Core;

namespace Match3Input
{
    public abstract class InputState
    {
        protected InputController controller;
        protected InputStateMachine stateMachine;

        protected InputState(InputController controller, InputStateMachine stateMachine)
        {
            this.controller = controller;
            this.stateMachine = stateMachine;  
        }

        public abstract void MakeTurn(Turn turn);

        protected void ChangeToCreateSlotState(Turn turn)
        {
            stateMachine.ChangeState(controller.createSlotState);
            controller.TurnMade(turn);
        }

        public void AbilityButtonPressed(int buttonId)
        {
            switch (buttonId)
            {
                case 0:
                    stateMachine.ChangeState(controller.turnState);
                    Debug.Log("THIS IS DEBUG");
                    break;
                case 1:
                    stateMachine.ChangeState(controller.destroyCellState);
                    break;
                case 2:
                    stateMachine.ChangeState(controller.destroySlotState);
                    break;
                case 3:
                    stateMachine.ChangeState(controller.createSlotState);
                    break;
                case 4:
                    stateMachine.ChangeState(controller.shockerState);
                    break;
                default:
                    throw new Exception($"Unknown ability button with id {buttonId} pressed");
            }
        }
    }
}