using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Match3Core;
using Match3Core.Levels;
using Match3Configs.Levels;
using System;

namespace Match3Debug.Configs
{
    public class LevelsConfigEditor : EditorWindow
    {
        private string _levelID = "";
        private Level _currentLevel;
        private HashSet<Level> _levels;
        private Vector2 _levelsScrollPosition;
        private Vector2 _gridScrollPosition;

        private BoardSlotWindow _boardSlotWindow;

        [MenuItem("Match3/LevelsEditor")]
        public static void ShowWindow()
        {
            GetWindow<LevelsConfigEditor>("Levels Editor");
        }

        private void OnGUI()
        {
            _levels = XmlBoardsReader.GetBoards();

            EditorGUILayout.BeginVertical();

            LevelFinder();

            EditorGUILayout.BeginHorizontal();

            LevelsScroll();

            AddNewLevelButton();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(25);

            _gridScrollPosition = GUILayout.BeginScrollView(_gridScrollPosition);

            if (_currentLevel != null)
            {
                GUILayout.Label("Win Condition: ", GUILayout.ExpandWidth(false));

                EditorGUILayout.BeginHorizontal();

                var condition = _currentLevel.condition;

                WinConditionCellsColor(condition);

                WinConditionSpecial(condition);

                WinConditionUnblcok(condition);

                if (GUILayout.Button($"Set Shape"))
                {

                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space(25);

                // Как-то медленно работает
                var slotsGrid = ScriptableObject.CreateInstance<BoardSlotEditor>();
                slotsGrid.CreateGrid(_currentLevel);
                
                ReadFromConfigCurrentLevel();
            }

            GUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private void LevelFinder()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Choose level: ", GUILayout.ExpandWidth(false));
            _levelID = GUILayout.TextField(_levelID, 20);
            EditorGUILayout.EndHorizontal();
        }

        private void LevelsScroll()
        {
            _levelsScrollPosition = GUILayout.BeginScrollView(_levelsScrollPosition, GUILayout.Width(200), GUILayout.Height(100));

            foreach (var l in _levels)
            {
                if (_levelID.Length == 0 || l.ID.ToString().Contains(_levelID))
                {
                    if (GUILayout.Button($"{l.ID}"))
                    {
                        BoardSlotWindow.TryToCloseWindow();
                        _currentLevel = l;
                    }
                }
            }

            GUILayout.EndScrollView();
        }

        private void AddNewLevelButton()
        {
            if (GUILayout.Button($"Add new level", GUILayout.Width(228)))
            {
                if (_levelID.Length > 0 && !_levels.Select(e => e.ID).Contains(Int32.Parse(_levelID)))
                {
                    XmlBoardsWriter.AddNewLevel(Int32.Parse(_levelID));
                }
            }
        }

        private void ReadFromConfigCurrentLevel()
        {
            _levels = XmlBoardsReader.GetBoards();
            foreach (var l in _levels)
            {
                if (l.ID == _currentLevel.ID)
                {
                    _currentLevel = l;
                }
            }
        }

        private void WinConditionCellsColor(Condition condition)
        {
            var cellCondition = (condition.flags & (byte)ConditionFlags.ColorCounter) > 0;
            if (GUILayout.Button($"Cell Count"))
            {
                XmlBoardsWriter.ToggleWinConditionColorCount(_currentLevel.ID);
            }
            if (cellCondition)
            {
                var stringToEdit = condition.colorCount.ToString();
                stringToEdit = GUILayout.TextField(stringToEdit, 25);
                if ((stringToEdit != condition.colorCount.ToString()) && (stringToEdit != ""))
                {
                    XmlBoardsWriter.SetWinConditionColorCount(_currentLevel.ID, stringToEdit);
                }
                ChooseCellColorField(condition);
            }
            else
            {
                GUILayout.Label("false");
            }
        }

        private void WinConditionSpecial(Condition condition)
        {
            if (GUILayout.Button($"Special Cells"))
            {
                XmlBoardsWriter.ToggleWinConditionSpecial(_currentLevel.ID);
            }
            GUILayout.Label(condition.special.ToString());
        }

        private void WinConditionUnblcok(Condition condition)
        {
            if (GUILayout.Button($"Unblock Cells"))
            {
                XmlBoardsWriter.ToggleWinConditionUnblock(_currentLevel.ID);
            }
            GUILayout.Label(condition.unblock.ToString());
        }

        private void ChooseCellColorField(Condition condition)
        {
            var options = Array.FindAll(Enum.GetNames(typeof(CellsColor)), 
                color => (color != Enum.GetName(typeof(CellsColor), CellsColor.Special)) && (color != Enum.GetName(typeof(CellsColor), CellsColor.Empty)));
            var color = condition?.color;
            var index = color is null ? 0 : (int)color - 2;
            var indexOriginal = index;
            index = EditorGUILayout.Popup(index, options, GUILayout.Width(70));
            if (index != indexOriginal)
            {
                XmlBoardsWriter.SetWinConditionColor(_currentLevel.ID, (CellsColor)(index + 2));
            }
        }
    }
}