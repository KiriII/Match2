using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;
using Match3Configs.Levels;
using Match3Core.gui;
using Match3Core.Levels;
using Match3Input;

public class Launcher : MonoBehaviour
{
    [SerializeField] Transform _canvas;
    [SerializeField] GameObject _mainUI;

    private Match3GameCore _match3GameCore;
    private LevelsHolder _levelsHolder;
    private GameObject mainUIObject;
    private ViewUpdateStack _viewUpdate;
    private InputController _inputController;

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
            _match3GameCore.FindTriples();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _inputController?.ChangeState(0);
        }

        _viewUpdate?.UpdateScreen();
    }

    private void CreateLevel(int levelNumber)
    {
        Destroy(mainUIObject);

        mainUIObject = Instantiate(_mainUI, _canvas);

        _levelsHolder.currentLevelID = _levelsHolder.GetLevel(levelNumber).ID;

        var ñurrentLevel = _levelsHolder.GetCurrentLevel();
        var ids = _levelsHolder.GetLevelsID();
        var boxes = ñurrentLevel.Boxes;

        _match3GameCore = new Match3GameCore(ñurrentLevel.Slots, boxes);

        _viewUpdate = new ViewUpdateStack(mainUIObject.GetComponent<WindowMatch3Debug>(), _match3GameCore);
        _inputController = new InputController(_match3GameCore);

        mainUIObject.GetComponent<WindowMatch3Debug>().init(_match3GameCore.GetBoardModel(), 
            ids, 
            _levelsHolder.currentLevelID, 
            CreateLevel,
            _inputController);
    }
}