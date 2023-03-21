using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Match3Core;
using Match3Core.MakeTurn;

public class DraggingCell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 initialPos;
    private event Action<Turn> _turnMade;

    public Coordinate coordinate;

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = eventData.pointerCurrentRaycast.screenPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPos = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnTurnMade(Calculate(Input.mousePosition));
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

        return new Turn(coordinate, vector);
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
