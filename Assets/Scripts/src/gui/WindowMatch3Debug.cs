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
        private Action<Coordinate> _destroyOneCellAction;

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
            UnityAction<int> createNewBoardAction, 
            Action<Coordinate> destroyOneCellAction)
        {
            _currentLevelID = curentLevelID;

            _destroyOneCellAction = destroyOneCellAction;

            _levelsID = levelsID;
            InitDropdownListView(createNewBoardAction);

            _GUIBoardModel = GUIBoardModel;

            var slots = _GUIBoardModel.GetSlots();
            _rows = _GUIBoardModel.GetRows();
            _collumns = _GUIBoardModel.GetCollumns();

            _slotsObjects = new GameObject[_rows, _collumns];

            _slotsGrid.constraintCount = _rows;

            DrawField();

            _updateViewButton.onClick.AddListener(UpdateView);
        }

        private void InitDropdownListView(UnityAction<int> createNewBoardAction)
        {
            var levelsIDToString = new List<String>(_levelsID.ToList().Select(id => id.ToString()));
            _levelsList.AddOptions(levelsIDToString);
            _levelsList.value = levelsIDToString.IndexOf(_currentLevelID.ToString());

            _levelsList.onValueChanged.AddListener(createNewBoardAction);
        }

        public void DrawField()
        {
            var gridTransform = _slotsGrid.gameObject.transform;
            for (int x = 0; x < _rows; x++)
            {
                for (int y = 0; y < _collumns; y++)
                {
                    DrawOneCell(new Coordinate(x, y), DrawOneSlot(new Coordinate(x, y), gridTransform));
                }
            }
        }

        public Transform DrawOneSlot(Coordinate coordinate, Transform parent)
        {
            var slotObject = Instantiate(_slot, parent);
            _slotsObjects[coordinate.x, coordinate.y] = slotObject;
            if (!_GUIBoardModel.GetCanHoldCell(coordinate.x, coordinate.y)) slotObject.GetComponent<Image>().enabled = false;
            return slotObject.transform;
        }

        public void DrawOneCell(Coordinate coordinate, Transform parent)
        {
            if (!_GUIBoardModel.GetCanHoldCell(coordinate.x, coordinate.y)) return;
            var cell = _GUIBoardModel.GetCell(coordinate.x, coordinate.y);
            var cellObject = Instantiate(_cell, _slotsObjects[coordinate.x, coordinate.y].transform);
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

            var cellButtonObject = cellObject.GetComponent<Button>();
            cellButtonObject.onClick.AddListener(delegate { DestroyCell(coordinate); });
        }

        public void UpdateView()
        {
            DestroyAllSlots();
            DrawField();
        }

        private void DestroyCell(Coordinate coordinate)
        {
            _destroyOneCellAction(coordinate);
        }

        public void DestroyAllSlots()
        {
            foreach (Transform s in _slotsGrid.transform)
            {
                Destroy(s.gameObject);
            }
        }
        
        public void DestroyWindow(bool destroyGameObject = true)
        {
            Destroy(this.gameObject);
        }
    }
}