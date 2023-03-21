using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggingCell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 initialPos;

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
        Calculate(Input.mousePosition);
    }

    private void Calculate(Vector3 finalPos)
    {
        float disX = Mathf.Abs(initialPos.x - finalPos.x);
        float disY = Mathf.Abs(initialPos.y - finalPos.y);
        if (disX > 15 || disY > 15)
        {
            if (disX > disY)
            {
                if (initialPos.x > finalPos.x)
                {
                    Debug.Log($"Left {disX}");
                }
                else
                {
                    Debug.Log($"Right {disX}");
                }
            }
            else
            {
                if (initialPos.y > finalPos.y)
                {
                    Debug.Log($"Down {disY}");
                }
                else
                {
                    Debug.Log($"Up {disY}");
                }
            }
        }
    }
}
