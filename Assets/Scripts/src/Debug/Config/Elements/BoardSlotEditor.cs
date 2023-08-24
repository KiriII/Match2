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
                    if (!level.slots[i, j].IsActive) slotSkin = GUI.skin.box;

                    EditorGUILayout.BeginVertical(slotSkin, GUILayout.Width(60), GUILayout.Height(60));
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("A:");
                    if (GUILayout.Button($"{level.slots[i, j].IsActive}", GUI.skin.label))
                    {
                        XmlBoardsWriter.ToggleActive(level.ID, i, j);
                    }
                    EditorGUILayout.EndHorizontal();
                    if (level.slots[i, j].IsActive)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label("H:");
                        if (GUILayout.Button($"{level.slots[i, j].CanHoldCell}", GUI.skin.label))
                        {
                            XmlBoardsWriter.ToggleHoldCell(level.ID, i, j);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    if (!level.slots[i, j].CanHoldCell || !level.slots[i, j].IsActive)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label("P:");
                        if (GUILayout.Button($"{level.slots[i, j].CanPassCell}", GUI.skin.label))
                        {
                            XmlBoardsWriter.TogglePassCell(level.ID, i, j);
                        }
                        EditorGUILayout.EndHorizontal();
                    } 
                    if (level.slots[i, j].CanHoldCell && level.slots[i, j].IsActive)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label("B:");
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
