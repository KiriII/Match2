using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Match3Core;
using Match3Core.Levels;
using Match3Configs.Levels;

namespace Match3Debug.Configs
{
    public class BoardSlotWindow : EditorWindow
    {
        private static BoardSlotWindow window;

        private static Slot _slot;
        private static int _levelId;

        private static int x;
        private static int y;

        public static BoardSlotWindow GetInstance(Slot slot, int levelId)
        {
            if (window != null) window.Close();
            window = CreateWindow(slot, levelId);
            return window;
        }

        public static void TryToCloseWindow()
        {
            if (window != null) window.Close();
        }

        private static BoardSlotWindow CreateWindow(Slot slot, int levelId)
        {
            window = (BoardSlotWindow)GetWindow(typeof(BoardSlotWindow));

            _slot = slot;
            _levelId = levelId;

            x = slot.Coordinate.x;
            y = slot.Coordinate.y;

            window.titleContent = new GUIContent($"cell pos {x} {y}");
            window.Show();
            return window;
        }

        void OnDestroy()
        {
            window = null;
        }

        void OnGUI()
        {
            DrawActiveField();
            if (_slot.IsActive)
            {
                DrawHoldCellField();
            }
            else
            {
                DrawBoxField();
            }
            if (!_slot.CanHoldCell || !_slot.IsActive)
            {
                DrawPassCellField();
            }
            if (_slot.CanHoldCell && _slot.IsActive)
            {
                DrawBlockedField();
                if (!_slot.IsBlocked)
                {
                    DrawCellColorField();
                }
            }
        }

        private void DrawActiveField()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Active:");
            if (GUILayout.Button($"{_slot.IsActive}", GUI.skin.label))
            {
                XmlBoardsWriter.ToggleActive(_levelId, x, y);
                _slot.IsActive = !_slot.IsActive;
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawHoldCellField()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Hold cell:");
            if (GUILayout.Button($"{_slot.CanHoldCell}", GUI.skin.label))
            {
                XmlBoardsWriter.ToggleHoldCell(_levelId, x, y);
                XmlBoardsWriter.SetColor(_levelId, x, y, CellsColor.Empty);

                _slot.CanHoldCell = !_slot.CanHoldCell;
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawPassCellField()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Pass cell:");
            if (GUILayout.Button($"{_slot.CanPassCell}", GUI.skin.label))
            {
                XmlBoardsWriter.TogglePassCell(_levelId, x, y);

                _slot.CanPassCell = !_slot.CanPassCell;
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawBlockedField()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Blocked:");
            if (GUILayout.Button($"{_slot.IsBlocked}", GUI.skin.label))
            {
                XmlBoardsWriter.ToggleBlocked(_levelId, x, y);

                _slot.IsBlocked = !_slot.IsBlocked;
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawCellColorField()
        {
            var options = Enum.GetNames(typeof(CellsColor));
            var color = _slot?.Cell?.Color;
            var index = color is null ? 0 : (int)color;
            var indexOriginal = index;
            index = EditorGUILayout.Popup(index, options);
            if (index != indexOriginal)
            {
                XmlBoardsWriter.SetColor(_levelId, x, y, (CellsColor)index);
                _slot.Cell = new Cell((CellsColor)index);
            }
        }

        private void DrawBoxField()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Box:");
            var boxId = XmlBoardsReader.TryGetBoxIdFromSlot(_slot.Coordinate, _levelId);
            var haveBox = boxId != null;
            if (GUILayout.Button($"{haveBox}", GUI.skin.label))
            {
                XmlBoardsWriter.ToggleBox(_levelId, x, y);
            }
            
            if (boxId != null)
            {
                var stringToEdit = boxId;
                stringToEdit = GUILayout.TextField(stringToEdit, 25);
                if (stringToEdit != boxId)
                {
                    XmlBoardsWriter.SetBoxId(_levelId, x, y, stringToEdit);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}