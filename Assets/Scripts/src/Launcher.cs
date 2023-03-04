using Match3Core;
using Match3Configs.Reader;
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

    void Start()
    {
        var mainUI = Instantiate(_mainUI, _canvas);

        var levels = XmlBoardsReader.GetBoards();
        _levelsHolder = new LevelsHolder(levels);

        // ------ DEBUG -----
        var level1 = _levelsHolder.GetLevelById(1);

        _match3GameCore = new Match3GameCore(level1.slots);

        mainUI.GetComponent<WindowMatch3Debug>().init(_match3GameCore.GetBoardModel());

        // _match3GameCore.DEADCELLS(new List<Coordinate> { new Coordinate(2, 1), new Coordinate(2, 2), new Coordinate(2, 3) });
    }
}