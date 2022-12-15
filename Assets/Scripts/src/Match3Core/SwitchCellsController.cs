using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Src.Match3Core
{
    public class SwitchCellsController
    {
        private CheckAfterTurnController _checkAfterTurnController;

        private ISwitchCellsModel _switchCellsModel;

        public SwitchCellsController(ISwitchCellsModel switchCellsModel,
            CheckAfterTurnController checkAfterTurnController)
        {
            _switchCellsModel = switchCellsModel;
            _checkAfterTurnController = checkAfterTurnController;
        }

        public void Turn((int x, int y) switchCell1, (int x, int y) switchCell2)
        {
            var newBoard = new Match3Model(_switchCellsModel.GetBoard());
            newBoard.SwitchTwoCells(switchCell1, switchCell2);

            var checkNewBoard = new CheckSameOnBoardController(newBoard);
            Debug.Log(newBoard.Print());
            var findedTriplesOnNewBoard = checkNewBoard.CheckSame();
            Debug.Log(String.Join(", ", findedTriplesOnNewBoard));
            if (findedTriplesOnNewBoard.Count == 0) return;
            _switchCellsModel.SwitchTwoCells(switchCell1, switchCell2);
            _checkAfterTurnController.CheckSameAfterTurn();
        }
    }
}