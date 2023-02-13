using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Match3Core;
using Match3.Board;

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
        if (GUILayout.Button("RESET"))
        {
            _match3GameCore = new Match3GameCore(5, 5);
            _match3GameCore.PrintCurrnetBoard();
        }

        if (GUILayout.Button("CHECK Tripless"))
        {
            deadCells = new List<(int, int)>(_match3GameCore.Check());
        }

        if (GUILayout.Button("Remove dead cells"))
        {
            _match3GameCore.FindDestroyedCellsInCollumns(deadCells);
            _match3GameCore.PrintCurrnetBoard();
        }


        using (var h = new GUILayout.HorizontalScope())
        {
            GUILayout.Label("Cell 1: ");
            x1 = (string)EditorGUILayout.TextField("x", x1);
            y1 = (string)EditorGUILayout.TextField("y", y1);
        }

        using (var h = new GUILayout.HorizontalScope())
        {
            GUILayout.Label("Cell 2: ");
            x2 = (string)EditorGUILayout.TextField("x", x2);
            y2 = (string)EditorGUILayout.TextField("y", y2);
        }

        if (GUILayout.Button("Make a Turn"))
        {
            if (x1.Length > 0 && y1.Length > 0 && x2.Length > 0 && y2.Length > 0)
            {
                switchCell1.x = int.Parse(x1);
                switchCell1.y = int.Parse(y1);

                switchCell2.x = int.Parse(x2);
                switchCell2.y = int.Parse(y2);

                _match3GameCore.Turn(switchCell1, switchCell2);
            }
        }

        GUILayout.Label("------- NEW START ---------");

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
            var board = new bool[,] { { true, false, false }, { true, false, false }, { true, false, false } };
            BoardModel boardModel = new BoardModel(board);
            boardModel.SetCell(new Coordinate(0, 0), new Cell(CellsColor.Blue));
            boardModel.SetCell(new Coordinate(1, 0), new Cell(CellsColor.Green));

            //boardModel.PrintCurrnetBoard();

            //var line = new FallingLine(new List<Coordinate> { new Coordinate(0, 0) , new Coordinate(1, 0) , new Coordinate(2, 0) }, boardModel);
                //line.Fall();
            boardModel.PrintCurrnetBoard();
        }

        if (GUILayout.Button("Falling Only Down"))
        {
            var board = new bool[,] { { true, false, true }, { true, true, true }, { true, true, false } };
            BoardModel boardModel = new BoardModel(board);
            boardModel.SetCell(new Coordinate(0, 0), new Cell(CellsColor.Blue));
            boardModel.SetCell(new Coordinate(1, 0), new Cell(CellsColor.Green));
            boardModel.SetCell(new Coordinate(1, 1), new Cell(CellsColor.Blue));
            boardModel.SetCell(new Coordinate(2, 1), new Cell(CellsColor.Green));
            boardModel.SetCell(new Coordinate(0, 2), new Cell(CellsColor.Blue));
            boardModel.SetCell(new Coordinate(1, 2), new Cell(CellsColor.Green));

            //var fallCtrl = new FallingOnlyDownController(boardModel);
            //fallCtrl.FallAllLines();
            boardModel.PrintCurrnetBoard();
        }
    }
}