using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Match3Debug.Configs
{
    public class SlotGUIElement : EditorWindow
    {
        public static void ShowWindow()
        {
            GetWindow<LevelsConfigEditor>("Levels Editor");
        }
    }
}