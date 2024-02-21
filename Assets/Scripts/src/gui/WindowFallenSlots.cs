using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Match3Core.FallenOff;
using Match3Input;

namespace Match3Core.gui
{
    public class WindowFallenSlots : MonoBehaviour
    {
        [SerializeField] private GameObject _fallenSlot;
        [SerializeField] private GameObject _cell;

        private Match3GameCore _gameCore;

        public void Awake()
        {
            if (_fallenSlot == null) throw new Exception($"Missing component in {this.gameObject.name}");
        }

        public void init(Match3GameCore gameCore)
        {
            _gameCore = gameCore;
        }

        public void DestroyFallenSlot(GameObject fallenSlot)
        {
            Destroy(fallenSlot);
        }

        public void CreateFallenSlot(Vector3 position)
        {
            var slot = _gameCore.GetFallenSlot();
            var slotObject = DrawOneSlot(gameObject.transform, slot);
            DrawOneCell(slotObject, slot);
            slotObject.localPosition = position;
            slotObject.GetComponent<DragAndDrop>().SetCanvas(gameObject.transform.parent.gameObject.GetComponent<Canvas>());
            slotObject.GetComponent<FallenSlotHolder>().fallenSlot = slot;
        }

        private Transform DrawOneSlot(Transform parent, Slot slotScreen)
        {
            var slotObject = Instantiate(_fallenSlot, parent);

            return slotObject.transform;
        }

        //Copy from WindowMatch3debug
        private void DrawOneCell(Transform parent, Slot slotScreen)
        {
            if (!slotScreen.CanHoldCell) return;
            var cell = slotScreen.Cell;
            var cellObject = Instantiate(_cell, parent).transform;
            if (cell.Color == CellsColor.Special)
            {
                var cellImageObject = Instantiate(cellObject.GetComponent<CellElement>().SpecialState, cellObject);
            }
            else
            {
                var cellImageObject = Instantiate(cellObject.GetComponent<CellElement>().NormalState, cellObject);
                var image = cellImageObject.GetComponent<Image>();

                switch (cell.Color)
                {
                    case CellsColor.Red:
                        image.color = Color.red;
                        break;
                    case CellsColor.Green:
                        image.color = Color.green;
                        break;
                    case CellsColor.Blue:
                        image.color = Color.blue;
                        break;
                    case CellsColor.Yellow:
                        image.color = Color.yellow;
                        break;
                    case CellsColor.Empty:
                        image.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}