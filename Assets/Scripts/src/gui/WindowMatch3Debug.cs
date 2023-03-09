using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Match3Core.Board;

namespace Match3Core.gui
{
    public class WindowMatch3Debug : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _slotsGrid;
        [SerializeField] private GameObject _slot;
        [SerializeField] private GameObject _cell;
        [SerializeField] private Button _updateViewButton;
        [SerializeField] private Dropdown _levelsList;

        private IGUIBoardModel _GUIBoardModel;
        private int _rows;
        private int _collumns;
        private int _currentLevelID;
        private HashSet<int> _levelsID;

        private GameObject[,] _slotsObjects;

        public void Awake()
        {
            if (_slotsGrid == null) throw new Exception($"Missing component in {this.gameObject.name}");
            if (_slot == null) throw new Exception($"Missing component in {this.gameObject.name}");
            if (_cell == null) throw new Exception($"Missing component in {this.gameObject.name}");
            if (_updateViewButton == null) throw new Exception($"Missing component in {this.gameObject.name}");
            if (_levelsList == null) throw new Exception($"Missing component in {this.gameObject.name}");
        }

        public void init(IGUIBoardModel GUIBoardModel, 
            HashSet<int> levelsID, 
            int curentLevelID,
            UnityAction<int> createNewBoardAction)
        {
            _currentLevelID = curentLevelID;

            _levelsID = levelsID;
            InitDropdownListView(createNewBoardAction);

            _GUIBoardModel = GUIBoardModel;

            var slots = _GUIBoardModel.GetSlots();
            _rows = _GUIBoardModel.GetRows();
            _collumns = _GUIBoardModel.GetCollumns();

            _slotsObjects = new GameObject[_rows, _collumns];

            _slotsGrid.constraintCount = _rows;

            DrawSlots();
            DrawCells();

            _updateViewButton.onClick.AddListener(UpdateView);
        }

        private void InitDropdownListView(UnityAction<int> createNewBoardAction)
        {
            var levelsIDToString = new List<String>(_levelsID.ToList().Select(id => id.ToString()));
            _levelsList.AddOptions(levelsIDToString);
            _levelsList.value = levelsIDToString.IndexOf(_currentLevelID.ToString());

            _levelsList.onValueChanged.AddListener(createNewBoardAction);
        }

        private void DrawSlots()
        {
            var gridTransform = _slotsGrid.gameObject.transform; 

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _collumns; j++)
                {
                    var slotObject = Instantiate(_slot, gridTransform);
                    _slotsObjects[i, j] = slotObject;
                    if (!_GUIBoardModel.GetCanHoldCell(i, j)) slotObject.GetComponent<Image>().enabled = false;
                }
            }
        } 

        public void DrawCells()
        {
            Debug.Log("DRAW CELLS");
            var gridTransform = _slotsGrid.gameObject.transform;

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _collumns; j++)
                {
                    if (!_GUIBoardModel.GetCanHoldCell(i, j)) continue;
                    var cell = _GUIBoardModel.GetCell(i, j);
                    var cellObject = Instantiate(_cell, _slotsObjects[i, j].transform);
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
                }
            }
        }

        public void UpdateView()
        {
            DrawCells();
        }

        public void Destroy(bool destroyGameObject = true)
        {
            Destroy(this.gameObject);
        }
    }
}