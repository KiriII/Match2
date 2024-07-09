using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Match3Input
{
    public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private Rigidbody2D _rigidbody;

        private Canvas _canvas;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _rigidbody.simulated = false;
            _canvasGroup.alpha = .6f;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _rigidbody.simulated = true;
            _canvasGroup.alpha = 1f;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_canvas == null) throw new Exception("Canvas not found");
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
        }

        public void SetCanvas(Canvas canvas)
        {
            _canvas = canvas;
        }
    }
}