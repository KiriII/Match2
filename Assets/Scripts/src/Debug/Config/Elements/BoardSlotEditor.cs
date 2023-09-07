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
                    EditorGUILayout.BeginVertical(slotSkin, GUILayout.Width(70), GUILayout.Height(60));
                    EditorGUILayout.Space();
                    if (GUILayout.Button($"edit", GUI.skin.button))
                    {
                        var window = BoardSlotWindow.GetInstance(level.slots[i,j], level.ID);
                    }

                    if (level.slots[i, j].CanHoldCell && level.slots[i, j].IsActive)
                    {
                        if (level.slots[i, j].IsBlocked)
                        {
                            EditorGUILayout.ColorField(Color.black);
                        }
                        else
                        {
                            switch (level.slots[i, j].Cell?.Color)
                            {
                                case CellsColor.Special:
                                    EditorGUILayout.ColorField(Color.white);
                                    break;
                                case CellsColor.Red:
                                    EditorGUILayout.ColorField(Color.red);
                                    break;
                                case CellsColor.Green:
                                    EditorGUILayout.ColorField(Color.green);
                                    break;
                                case CellsColor.Blue:
                                    EditorGUILayout.ColorField(Color.blue);
                                    break;
                                case CellsColor.Yellow:
                                    EditorGUILayout.ColorField(Color.yellow);
                                    break;
                                default:
                                    EditorGUILayout.ColorField(Color.gray);
                                    break;
                            }
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
