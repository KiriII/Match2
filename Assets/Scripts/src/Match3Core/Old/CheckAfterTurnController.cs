using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core
{
    public class CheckAfterTurnController
    {
        private FindDestroyedCellsController _findDestroyedCellsController;
        private CheckSameOnBoardController _checkAfterTurnController;

        private Match3Model _match3Model;

        public CheckAfterTurnController(FindDestroyedCellsController findDestroyedCellsController, 
            CheckSameOnBoardController checkSameOnBoardController,
            Match3Model match3Model)
        {
            _findDestroyedCellsController = findDestroyedCellsController;
            _checkAfterTurnController = checkSameOnBoardController;
            _match3Model = match3Model;
        }

        public void CheckSameAfterTurn()
        {
            while (_checkAfterTurnController.CheckSame().Count > 0)
            {
                var findedTriplesOnBoard = _checkAfterTurnController.CheckSame();
                _findDestroyedCellsController.FindDestroyedCellsInCollumns(findedTriplesOnBoard, _match3Model.GetRowsCount());
            }
        }
    }
}