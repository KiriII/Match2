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
    [SerializeField] private Transform _canvas;
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _fallenSlots;

    private Match3GameCore _match3GameCore;
    private LevelsHolder _levelsHolder;
    private GameObject _mainUIObject;
    private GameObject _fallenSlotsObject;
    private ViewUpdateStack _viewUpdate;
    private InputController _inputController;

    void Start()
    {
        var levels = XmlBoardsReader.GetBoards();
        _levelsHolder = new LevelsHolder(levels, "0");
        CreateLevel(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _inputController?.AbilityButtonPressed(0);
        }
        _viewUpdate?.UpdateScreen();
    }

    private void CreateLevel(int levelId)
    {
        Destroy(_mainUIObject);
        Destroy(_fallenSlotsObject);

        _mainUIObject = Instantiate(_mainUI, _canvas);
        _fallenSlotsObject = Instantiate(_fallenSlots, _canvas);

        _levelsHolder.currentLevelID = _levelsHolder.GetLevel(levelId).ID;

        var ņurrentLevel = _levelsHolder.GetCurrentLevel();
        var ids = _levelsHolder.GetAwaibleLevelsId();
        var boxes = ņurrentLevel.Boxes;

        _match3GameCore = new Match3GameCore(ņurrentLevel.Slots, ņurrentLevel.condition, _levelsHolder.CurrentLevelComplete, CreateNextLevel);

        _viewUpdate = new ViewUpdateStack(_mainUIObject.GetComponent<WindowMatch3Debug>(), _match3GameCore);
        _inputController = new InputController(_match3GameCore);

        _inputController.EnableSlotDestroyedListener(_fallenSlotsObject.GetComponent<WindowFallenSlots>().CreateFallenSlot);
        _inputController.EnableSlotCreatedListener(_fallenSlotsObject.GetComponent<WindowFallenSlots>().DestroyFallenSlot);

        _fallenSlotsObject.GetComponent<WindowFallenSlots>().init(_match3GameCore);

        _mainUIObject.GetComponent<WindowMatch3Debug>().init(_match3GameCore,
            ids,
            _levelsHolder.currentLevelID,
            CreateLevel,
            _inputController);
    }

    private void CreateNextLevel()
    {
        CreateLevel(_levelsHolder.GetCurrentLevelIndex() + 1);
    }
}