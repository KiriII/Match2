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
        public void CreateGrid(Level level, HashSet<Coordinate> shape)
        {
            if (level == null) return;
            for (int i = 0; i < level.rows; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < level.collumns; j++)
                {
                    if (shape.Contains(level.Slots[i, j].Coordinate)) GUI.skin = (GUISkin)AssetDatabase.LoadAssetAtPath("Assets/ShapeCondition.guiskin", typeof(GUISkin));
                    var slotSkin = level.Slots[i, j].CanHoldCell ? GUI.skin.button : GUI.skin.textArea;
                    if (!level.Slots[i, j].IsActive) slotSkin = GUI.skin.box;
                    EditorGUILayout.BeginVertical(slotSkin, GUILayout.Width(75), GUILayout.Height(75));
                    GUI.skin = null;
                    EditorGUILayout.Space();
                    if (GUILayout.Button($"edit", GUI.skin.button))
                    {
                        var window = BoardSlotWindow.GetInstance(level.Slots[i,j], level.ID);
                    }

                    if (level.Slots[i, j].CanHoldCell && level.Slots[i, j].IsActive)
                    {
                        if (level.Slots[i, j].IsBlocked)
                        {
                            EditorGUILayout.ColorField(Color.black);
                        }
                        else
                        {
                            switch (level.Slots[i, j].Cell?.Color)
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

                    // Shape condition 
                    EditorGUILayout.BeginHorizontal(GUILayout.Width(50));
                    var shapeCond = (shape != null) && (shape.Count > 0);
                    if (shape.Count > 0)
                    {
                        var InShapeCondtion = shape.Contains(level.Slots[i, j].Coordinate);
                        GUILayout.Label("Shape");
                        var shapeNeeded = InShapeCondtion;
                        shapeNeeded = EditorGUILayout.Toggle(shapeNeeded, GUILayout.ExpandWidth(false));
                        if (shapeNeeded != InShapeCondtion) 
                        {
                            XmlBoardsWriter.ToggleShapeByPosition(level.ID, i, j);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
