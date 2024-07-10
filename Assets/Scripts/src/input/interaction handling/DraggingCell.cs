using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Match3Core;
using Match3Core.gui;

namespace Match3Input
{
    public class DraggingCell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDropHandler
    {
        private Vector2 initialPos;
        private event Action<Turn> _turnMade;

        public Coordinate coordinate;

        public void OnPointerDown(PointerEventData eventData)
        {
            initialPos = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnTurnMade(Calculate(eventData.position));
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log($"Drop {eventData.pointerDrag}");
            var fallenSlotObject = eventData.pointerDrag;
            OnTurnMade(new Turn(coordinate, fallenSlotObject.GetComponent<FallenSlotHolder>().fallenSlot, fallenSlotObject));
        }

        private Turn Calculate(Vector3 finalPos)
        {
            float disX = Mathf.Abs(initialPos.x - finalPos.x);
            float disY = Mathf.Abs(initialPos.y - finalPos.y);

            var vector = Vector2.zero;

            if (disX > 15 || disY > 15)
            {
                if (disX > disY)
                {
                    if (initialPos.x > finalPos.x)
                    {
                        vector = Vector2.left;
                        //Debug.Log($"Left {disX}");
                    }
                    else
                    {
                        vector = Vector2.right;
                        //Debug.Log($"Right {disX}");
                    }
                }
                else
                {
                    if (initialPos.y > finalPos.y)
                    {
                        vector = Vector2.down;
                        //Debug.Log($"Down {disY}");
                    }
                    else
                    {
                        vector = Vector2.up;
                        //Debug.Log($"Up {disY}");
                    }
                }
            }
            var localPosition = gameObject.transform.localPosition;
            return new Turn(coordinate, vector, new Vector3(localPosition.x, localPosition.y, localPosition.z)); ;
        }

        private void OnTurnMade(Turn turn)
        {
            _turnMade?.Invoke(turn);
        }

        public void EnableTurnMadeListener(Action<Turn> methodInLitener)
        {
            _turnMade += methodInLitener;
        }

        public void DesableTurnMadeListener(Action<Turn> methodInLitener)
        {
            _turnMade -= methodInLitener;
        }
    }
}