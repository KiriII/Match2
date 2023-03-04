using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Match3Core;
using Match3Core.Board;
using Match3Core.Falling;
using Match3Core.DestroyCells;

public class Match3EditorWindow : EditorWindow
{
    private Match3GameCore _match3GameCore;

    private List<(int, int)> deadCells = new List<(int x, int y)>();
    private (int x, int y) switchCell1;
    private (int x, int y) switchCell2;
    private string x1;
    private string y1;
    private string x2;
    private string y2;

    [MenuItem("Match3/CoreTest")]
    public static void ShowWindow()
    {
        GetWindow<Match3EditorWindow>("Core Gameplay Test");
    }

    private void OnGUI()
    {
        /*
        if (GUILayout.Button("NEW BOARD"))
        {
            var board = new bool[,] { { false, false, false }, { true, false, true }, { false, true, false } };
            BoardModel boardModel = new BoardModel(board);
            boardModel.SetCell(new Coordinate(1, 0), new Cell(CellsColor.Blue));
            boardModel.SetCell(new Coordinate(1, 2), new Cell(CellsColor.Green));
            boardModel.SetCell(new Coordinate(2, 1), new Cell(CellsColor.Red));
            boardModel.PrintCurrnetBoard();

            SwitchCellsContoller switchCellsContoller = new SwitchCellsContoller(boardModel);
            switchCellsContoller.SwitchCells(new Coordinate(1, 0), new Coordinate(1, 2));
            boardModel.PrintCurrnetBoard();

            switchCellsContoller.SwitchCells(new Coordinate(1, 2), new Coordinate(2, 1));
            boardModel.PrintCurrnetBoard();

            switchCellsContoller.SwitchCells(new Coordinate(1, 2), new Coordinate(2, 2)); // NOTHING HAPPENS
            boardModel.PrintCurrnetBoard();
        }

        if (GUILayout.Button("XXXTentacion Falling down"))
        {
            var board = new Slot[,] { {new Slot(false, false)}, { new Slot(true, true) }, { new Slot(true, true) }, { new Slot(true, true) }, { new Slot(true, true) } };
            BoardModel boardModel = new BoardModel(board);
            boardModel.SetCell(new Coordinate(1, 0), new Cell(CellsColor.Blue));
            boardModel.SetCell(new Coordinate(2, 0), new Cell(CellsColor.Green));

            SwitchCellsContoller switchCellsContoller = new SwitchCellsContoller(boardModel);

            boardModel.PrintCurrnetBoard();

            var fallCtrl = new FallingController(boardModel, switchCellsContoller);
            fallCtrl.FallingWithDeadCells(new List<Coordinate> { new Coordinate(0, 0), new Coordinate(3,0), new Coordinate(4, 0)});

            boardModel.PrintCurrnetBoard();
        }

        if (GUILayout.Button("Destroy"))
        {
            var board = new Slot[,] { { new Slot(true, true) }, { new Slot(true, true) }, { new Slot(true, true) }, { new Slot(true, true) }, { new Slot(true, true) } };
            BoardModel boardModel = new BoardModel(board);
            boardModel.SetCell(new Coordinate(0, 0), new Cell(CellsColor.Blue));
            boardModel.SetCell(new Coordinate(1, 0), new Cell(CellsColor.Green));
            boardModel.SetCell(new Coordinate(2, 0), new Cell(CellsColor.Blue));
            boardModel.SetCell(new Coordinate(3, 0), new Cell(CellsColor.Green));
            boardModel.SetCell(new Coordinate(4, 0), new Cell(CellsColor.Red));

            SwitchCellsContoller switchCellsContoller = new SwitchCellsContoller(boardModel);

            boardModel.PrintCurrnetBoard();

            var cellsDestroyController = new CellsDestroyController(switchCellsContoller);
            var fallCtrl = new FallingController(boardModel, switchCellsContoller);

            cellsDestroyController.EnableDestroyedCellDestroyedListener(fallCtrl.FallingWithDeadCells);

            cellsDestroyController.DestroyCells(new List<Coordinate> { new Coordinate(4, 0), new Coordinate(3, 0), new Coordinate(0, 0) });

            boardModel.PrintCurrnetBoard();
        }
        */
    }
}