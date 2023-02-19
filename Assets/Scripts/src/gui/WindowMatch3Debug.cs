using Match3Core.Board;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Core.gui
{
    public class WindowMatch3Debug : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _slotsGrid;
        [SerializeField] private GameObject _slot;
        [SerializeField] private GameObject _cell;

        private IGUIBoardModel _GUIBoardModel;

        public void Awake()
        {
            if (_slotsGrid == null) throw new Exception($"Missing component in {this.gameObject.name}");
            if (_slot == null) throw new Exception($"Missing component in {this.gameObject.name}");
            if (_cell == null) throw new Exception($"Missing component in {this.gameObject.name}");
        }

        public void init(IGUIBoardModel GUIBoardModel)
        {
            _GUIBoardModel = GUIBoardModel;

            var slots = _GUIBoardModel.GetSlots();
            var rows = _GUIBoardModel.GetRows();
            var collumns = _GUIBoardModel.GetCollumns();

            _slotsGrid.constraintCount = rows;
            var gridTransform = _slotsGrid.gameObject.transform;

            Debug.Log($"{slots} {rows} {collumns}");

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < collumns; j++)
                {
                    var slotObject = Instantiate(_slot, gridTransform);
                    if (_GUIBoardModel.GetCanHoldCell(i, j))
                    {
                        var cell = _GUIBoardModel.GetCell(i, j);
                        var cellObject = Instantiate(_cell, slotObject.transform);
                        var cellColorObject = cellObject.GetComponent<Image>();
                        
                        switch (cell.color)
                        {
                            case CellsColor.Red: 
                                cellColorObject.color = Color.red;
                                break;
                            case CellsColor.Green:
                                cellColorObject.color = Color.green;
                                break;
                            case CellsColor.Blue:
                                cellColorObject.color = Color.blue;
                                break;
                            case CellsColor.Empty:
                                cellColorObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                                break;
                            default:
                                break;
                        }
                    } else
                    {
                        slotObject.GetComponent<Image>().enabled = false;
                    }
                }
            }
        }

        public void Destroy(bool destroyGameObject = true)
        {
            Destroy(this.gameObject);
        }
    }
}