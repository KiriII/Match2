using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
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

            if (_currentLevel != null)
            {
                // Как-то медленно работает
                var slotsGrid = ScriptableObject.CreateInstance<BoardSlotEditor>();
                slotsGrid.CreateGrid(_currentLevel);
                ReadFromConfigCurrentLevel();
            }

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
    }
}