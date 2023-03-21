using Match3Core;
using Match3Configs.Levels;
using Match3Core.gui;
using Match3Core.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] Transform _canvas;
    [SerializeField] GameObject _mainUI;

    private Match3GameCore _match3GameCore;
    private LevelsHolder _levelsHolder;
    private GameObject mainUIObject;

    void Start()
    {
        var levels = XmlBoardsReader.GetBoards();
        _levelsHolder = new LevelsHolder(levels, 0);

        CreateLevel(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //_match3GameCore.FindTriples();
        }
    }

    private void CreateLevel(int levelNumber)
    {
        Destroy(mainUIObject);

        mainUIObject = Instantiate(_mainUI, _canvas);

        _levelsHolder.currentLevelID = _levelsHolder.GetLevel(levelNumber).ID;

        var ñurrentLevel = _levelsHolder.GetCurrentLevel();
        var ids = _levelsHolder.GetLevelsID();

        _match3GameCore = new Match3GameCore(ñurrentLevel.slots, mainUIObject.GetComponent<WindowMatch3Debug>().UpdateView);

        mainUIObject.GetComponent<WindowMatch3Debug>().init(_match3GameCore.GetBoardModel(), 
            ids, 
            _levelsHolder.currentLevelID, 
            CreateLevel,
            _match3GameCore.DestroyCell);
    }
}