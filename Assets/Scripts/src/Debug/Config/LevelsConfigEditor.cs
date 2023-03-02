using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Match3Configs.Reader;
using Match3Core.Levels;
using Match3Configs.Writer;

namespace Match3Debug.Configs
{
    public class LevelsConfigEditor : EditorWindow
    {
        private string _levelID = "";
        private Level _currentLevel;
        private List<Level> _levels;
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

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Choose level: ", GUILayout.ExpandWidth(false));
            _levelID = GUILayout.TextField(_levelID, 20);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            _levelsScrollPosition = GUILayout.BeginScrollView(_levelsScrollPosition, GUILayout.Width(200), GUILayout.Height(100));

            foreach(var l in _levels)
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
            

            if (GUILayout.Button($"Add new level", GUILayout.Width(228)))
            {
                
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(25);

            if (_currentLevel != null)
            {
                for (int i = 0; i < _currentLevel.rows; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    for (int j = 0; j < _currentLevel.collumns; j++)
                    {
                        var slotSkin = _currentLevel.slots[i, j].CanHoldCell ? GUI.skin.button : GUI.skin.textArea;

                        EditorGUILayout.BeginVertical(slotSkin, GUILayout.Width(90), GUILayout.Height(70));
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label("Hold:");
                        if (GUILayout.Button($"{_currentLevel.slots[i, j].CanHoldCell}", GUI.skin.label))
                        {
                            XmlBoardsWriter.ToggleHoldCell(_currentLevel.ID, i, j);
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label("Pass:");
                        if (GUILayout.Button($"{_currentLevel.slots[i, j].CanPassCell}", GUI.skin.label))
                        {
                            XmlBoardsWriter.TogglePassCell(_currentLevel.ID, i, j);
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("<-->");

                if (GUILayout.Button($"+", GUILayout.Width(100)))
                {

                }

                if (GUILayout.Button($"-", GUILayout.Width(100)))
                {

                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("↑↓");

                if (GUILayout.Button($"+", GUILayout.Width(100)))
                {

                }

                if (GUILayout.Button($"-", GUILayout.Width(100)))
                {

                }

                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.EndVertical();
        }
    }
}