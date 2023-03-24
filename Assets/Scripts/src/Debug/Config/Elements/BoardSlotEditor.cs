using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
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

                    EditorGUILayout.BeginVertical(slotSkin, GUILayout.Width(90), GUILayout.Height(70));
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Hold:");
                    if (GUILayout.Button($"{level.slots[i, j].CanHoldCell}", GUI.skin.label))
                    {
                        XmlBoardsWriter.ToggleHoldCell(level.ID, i, j);
                    }
                    EditorGUILayout.EndHorizontal();

                    if (!level.slots[i, j].CanHoldCell)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label("Pass:");
                        if (GUILayout.Button($"{level.slots[i, j].CanPassCell}", GUI.skin.label))
                        {
                            XmlBoardsWriter.TogglePassCell(level.ID, i, j);
                        }
                        EditorGUILayout.EndHorizontal();
                    } 
                    else
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label("Blocked:");
                        if (GUILayout.Button($"{level.slots[i, j].IsBlocked}", GUI.skin.label))
                        {
                            XmlBoardsWriter.ToggleBlocked(level.ID, i, j);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
