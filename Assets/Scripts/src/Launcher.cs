using Match3Core;
using Match3Core.gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] Transform _canvas;
    [SerializeField] GameObject _mainUI;

    private Match3GameCore _match3GameCore;

    void Start()
    {
        var mainUI = Instantiate(_mainUI, _canvas);

        // ------ DEBUG -----
        _match3GameCore = new Match3GameCore(new Slot[,] {{ new Slot(false, true), new Slot(true, true), new Slot(true, true), new Slot(true, true), new Slot(false, true) },
        { new Slot(true, true) ,  new Slot(false, true) ,  new Slot(true, true) ,  new Slot(false, true) ,  new Slot(true, true) },
        { new Slot(true, true) ,  new Slot(true, true) ,  new Slot(true, true) ,  new Slot(true, true) ,  new Slot(true, true) },
        { new Slot(true, true) ,  new Slot(false, true) ,  new Slot(true, true) ,  new Slot(false, true) ,  new Slot(true, true) },
        { new Slot(false, true) ,  new Slot(true, true) ,  new Slot(true, true) ,  new Slot(true, true) ,  new Slot(false, true) } });

        var boardModel = _match3GameCore.GetBoardModel();

        boardModel.SetCell(new Coordinate(0, 1), new Cell(CellsColor.Green));
        boardModel.SetCell(new Coordinate(0, 2), new Cell(CellsColor.Blue));
        boardModel.SetCell(new Coordinate(0, 3), new Cell(CellsColor.Green));

        boardModel.SetCell(new Coordinate(1, 0), new Cell(CellsColor.Red));

        boardModel.SetCell(new Coordinate(1, 2), new Cell(CellsColor.Blue));

        boardModel.SetCell(new Coordinate(1, 4), new Cell(CellsColor.Red));

        boardModel.SetCell(new Coordinate(2, 0), new Cell(CellsColor.Red));
        boardModel.SetCell(new Coordinate(2, 1), new Cell(CellsColor.Empty));
        boardModel.SetCell(new Coordinate(2, 2), new Cell(CellsColor.Empty));
        boardModel.SetCell(new Coordinate(2, 3), new Cell(CellsColor.Empty));
        boardModel.SetCell(new Coordinate(2, 4), new Cell(CellsColor.Red));

        boardModel.SetCell(new Coordinate(3, 0), new Cell(CellsColor.Red));

        boardModel.SetCell(new Coordinate(3, 2), new Cell(CellsColor.Blue));

        boardModel.SetCell(new Coordinate(3, 4), new Cell(CellsColor.Red));

        boardModel.SetCell(new Coordinate(4, 1), new Cell(CellsColor.Green));
        boardModel.SetCell(new Coordinate(4, 2), new Cell(CellsColor.Blue));
        boardModel.SetCell(new Coordinate(4, 3), new Cell(CellsColor.Green));


        mainUI.GetComponent<WindowMatch3Debug>().init(_match3GameCore.GetBoardModel());

        _match3GameCore.DEADCELLS(new List<Coordinate> { new Coordinate(2, 1), new Coordinate(2, 2), new Coordinate(2, 3) });
    }
}