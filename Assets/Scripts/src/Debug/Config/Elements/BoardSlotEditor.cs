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
    public class BoardSlotEditor : EditorWindow
    {
        public void CreateGrid(Level level)
        {
            if (level == null) return;
            for (int i = 0; i < level.rows; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < level.collumns; j++)
                {
                    var slotSkin = level.slots[i, j].CanHoldCell ? GUI.skin.button : GUI.skin.textArea;
                    if (!level.slots[i, j].IsActive) slotSkin = GUI.skin.box;
                    //slotSkin.normal.background = Texture2D.blackTexture;
                    EditorGUILayout.BeginVertical(slotSkin, GUILayout.Width(50), GUILayout.Height(50));
                    EditorGUILayout.Space();
                    if (GUILayout.Button($"edit", GUI.skin.button))
                    {
                        var window = new BoardSlotWindow(level.slots[i,j], level.ID);
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }

    public class BoardSlotWindow : EditorWindow
    {
        private Slot _slot;
        private int _levelId;

        private int x;
        private int y;

        public BoardSlotWindow(Slot slot, int levelId)
        {
            _slot = slot;
            _levelId = levelId;

            x = slot.Coordinate.x;
            y = slot.Coordinate.y;

            var window = GetWindow(typeof(BoardSlotWindow));
            window.Show();
        }

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("A:");
            if (GUILayout.Button($"{_slot.IsActive}", GUI.skin.label))
            {
                XmlBoardsWriter.ToggleActive(_levelId, x, y);
                _slot.IsActive = !_slot.IsActive;
            }
            EditorGUILayout.EndHorizontal();
            if (_slot.IsActive)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("H:");
                if (GUILayout.Button($"{_slot.CanHoldCell}", GUI.skin.label))
                {
                    XmlBoardsWriter.ToggleHoldCell(_levelId, x, y);
                    XmlBoardsWriter.SetColor(_levelId, x, y, CellsColor.Empty);
                }
                EditorGUILayout.EndHorizontal();
            }
            if (!_slot.CanHoldCell || !_slot.IsActive)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("P:");
                if (GUILayout.Button($"{_slot.CanPassCell}", GUI.skin.label))
                {
                    XmlBoardsWriter.TogglePassCell(_levelId, x, y);
                }
                EditorGUILayout.EndHorizontal();
            }
            if (_slot.CanHoldCell && _slot.IsActive)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("B:");
                if (GUILayout.Button($"{_slot.IsBlocked}", GUI.skin.label))
                {
                    XmlBoardsWriter.ToggleBlocked(_levelId, x, y);
                }
                EditorGUILayout.EndHorizontal();

                var options = Enum.GetNames(typeof(CellsColor));
                var color = _slot?.Cell?.color;
                var index = color is null ? 0 : (int)color;
                var indexOriginal = index;
                index = EditorGUILayout.Popup(index, options);
                if (index != indexOriginal)
                {
                    XmlBoardsWriter.SetColor(_levelId, x, y, (CellsColor)index);
                }
            }
        }
    }
}
