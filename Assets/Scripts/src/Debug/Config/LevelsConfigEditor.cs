using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Match3Configs.Reader;
using Match3Core.Levels;

namespace Match3Debug.Configs
{
    public class LevelsConfigEditor : EditorWindow
    {
        private string _levelID = "";
        private Level _currentLevel;
        private List<Level> _levels;

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

            GUILayout.BeginScrollView(new Vector2(100, 0), GUILayout.Width(200), GUILayout.Height(100));

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

            if (_currentLevel != null)
            {
                for (int i = 0; i < _currentLevel.rows; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    for (int j = 0; j < _currentLevel.collumns; j++)
                    {
                        if (GUILayout.Button($"{_currentLevel.slots[i, j].CanHoldCell}"))
                        {
                            Debug.Log("TIK TIK TIK");
                             
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndVertical();
        }
    }
}