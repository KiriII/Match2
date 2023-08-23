using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Match3Core.Board;
using Match3Core.MakeTurn;
using Match3Input;

namespace Match3Core.gui
{
    public class WindowMatch3Debug : MonoBehaviour, IViewUpdater
    {
        [SerializeField] private GridLayoutGroup _slotsGrid;
        [SerializeField] private GameObject _slot;
        [SerializeField] private GameObject _cell;
        [SerializeField] private Button _updateViewButton;
        [SerializeField] private Button _createSlotButton;
        [SerializeField] private Button _destroySlotButton;
        [SerializeField] private Button _destroyCellButton;
        [SerializeField] private Dropdown _levelsList;

        private IGUIBoardModel _GUIBoardModel;
        private InputController _inputController;
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
            if (_createSlotButton == null) throw new Exception($"Missing component in {this.gameObject.name}");
            if (_destroySlotButton == null) throw new Exception($"Missing component in {this.gameObject.name}");
            if (_destroyCellButton == null) throw new Exception($"Missing component in {this.gameObject.name}");
            if (_levelsList == null) throw new Exception($"Missing component in {this.gameObject.name}");
        }

        public void init(IGUIBoardModel GUIBoardModel, 
            HashSet<int> levelsID, 
            int curentLevelID,
            UnityAction<int> createNewBoardAction,
            InputController inputController)
        {
            _currentLevelID = curentLevelID;

            _inputController = inputController;

            _levelsID = levelsID;
            InitDropdownListView(createNewBoardAction);

            _GUIBoardModel = GUIBoardModel;

            var slots = _GUIBoardModel.GetSlots();
            _rows = _GUIBoardModel.GetRows();
            _collumns = _GUIBoardModel.GetCollumns();

            _slotsObjects = new GameObject[_rows, _collumns];

            _slotsGrid.constraintCount = _rows;

            DrawField(_GUIBoardModel.GetSlots());

            _updateViewButton.onClick.AddListener(UpdateView);
            _createSlotButton.onClick.AddListener(ChangeStateCreateSlot);
            _destroySlotButton.onClick.AddListener(ChangeStateDestroySlot);
            _destroyCellButton.onClick.AddListener(ChangeStateDestroyCell);
        }

        private void InitDropdownListView(UnityAction<int> createNewBoardAction)
        {
            var levelsIDToString = new List<String>(_levelsID.ToList().Select(id => id.ToString()));
            _levelsList.AddOptions(levelsIDToString);
            _levelsList.value = levelsIDToString.IndexOf(_currentLevelID.ToString());

            _levelsList.onValueChanged.AddListener(createNewBoardAction);
        }

        public void DrawField(Slot[,] boardCopy)
        {
            var gridTransform = _slotsGrid.gameObject.transform;
            for (int x = 0; x < _rows; x++)
            {
                for (int y = 0; y < _collumns; y++)
                {
                    if (boardCopy is not null)
                    { 
                        DrawOneCell(DrawOneSlot(new Coordinate(x, y), gridTransform, boardCopy[x, y]), boardCopy[x, y]);
                    }
                }
            }
        }

        private Transform DrawOneSlot(Coordinate coordinate, Transform parent, Slot slotScreen)
        {
            var slotObject = Instantiate(_slot, parent);
            _slotsObjects[coordinate.x, coordinate.y] = slotObject;
            if (!slotScreen.CanHoldCell)
            {
                var colorHalfVision = slotObject.GetComponent<Image>().color;
                colorHalfVision.a = 0.5f;
                slotObject.GetComponent<Image>().color = colorHalfVision;
            }
            if (slotScreen.IsBlocked) slotObject.GetComponent<Image>().color = Color.black;

            var draggingCell = slotObject.GetComponent<DraggingCell>();
            draggingCell.coordinate = coordinate;
            draggingCell.EnableTurnMadeListener(_inputController.TurnMade);

            return slotObject.transform;
        }

        private void DrawOneCell(Transform parent, Slot slotScreen)
        {
            if (!slotScreen.CanHoldCell) return;
            var cell = slotScreen.Cell;
            var cellObject = Instantiate(_cell, parent).transform;
            if (cell.color == CellsColor.Special)
            {
                var cellImageObject = Instantiate(cellObject.GetComponent<CellElement>().SpecialState, cellObject);
            }
            else
            {
                var cellImageObject = Instantiate(cellObject.GetComponent<CellElement>().NormalState, cellObject);
                var image = cellImageObject.GetComponent<Image>();

                switch (cell.color)
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

        
        public void UpdateView()
        {
            UpdateView(_GUIBoardModel.GetSlots());
        }
        
        public void ChangeStateDestroyCell()
        {
            _inputController.ChangeState(1);
        }

        public void ChangeStateDestroySlot()
        {
            _inputController.ChangeState(2);
        }

        public void ChangeStateCreateSlot()
        {
            _inputController.ChangeState(3);
        }

        public void UpdateView(Slot[,] boardCopy)
        {
            DestroyAllSlots();
            DrawField(boardCopy);
        }


        private void DestroyAllSlots()
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