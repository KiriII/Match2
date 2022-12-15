using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Src.Match3Core
{
    public class Match3GameCore : IMatch3GameCore
    {
        private FallCellsController _match3FallController;
        private AddCellsController _match3AddCellsController;
        private FindDestroyedCellsController _findDestroyedCellsController;
        private CheckSameOnBoardController _checkSameOnBoardController;
        private DestroyCellsController _destroyCellsController;
        private SwitchCellsController _switchCellsController;
        private CheckAfterTurnController _checkAfterTurnController;

        private Match3Model _match3Model;

        public Match3GameCore(int x, int y)  
        {
            _match3Model = new Match3Model(x, y);

            _findDestroyedCellsController = new FindDestroyedCellsController();
            _match3FallController = new FallCellsController(_match3Model);
            _match3AddCellsController = new AddCellsController(_match3Model);
            _destroyCellsController = new DestroyCellsController(_match3Model);
            _checkSameOnBoardController = new CheckSameOnBoardController(_match3Model);
            _checkAfterTurnController = new CheckAfterTurnController(_findDestroyedCellsController, _checkSameOnBoardController, _match3Model);
            _switchCellsController = new SwitchCellsController(_match3Model, _checkAfterTurnController);

            _match3AddCellsController.FillBoardWithNewCells();

            _findDestroyedCellsController.EnableDestroyedCellFindListener(_destroyCellsController.DestroyCell);
            _findDestroyedCellsController.EnableDestroyedCellDestroyedListener(_match3FallController.CellsFall);
            _findDestroyedCellsController.EnableDestroyedCellDestroyedListener(_match3AddCellsController.AddNewCells);

        }

        public void Turn((int x1, int y1) switchCell1, (int x2, int y2) switchCell2)
        {
            _switchCellsController.Turn(switchCell1, switchCell2);
        }

        public void FindDestroyedCellsInCollumns(List<(int x, int y)> destroyedCells)
        {
            var rowCount = _match3Model.GetRowsCount();
            _findDestroyedCellsController.FindDestroyedCellsInCollumns(destroyedCells, rowCount);
        }

        // ----- debug -----
        public void PrintCurrnetBoard()
        {
            Debug.Log(_match3Model.Print());
        }

        public List<(int, int)> Check()
        {
            Debug.Log(String.Join(", ", _checkSameOnBoardController.CheckSame()));
            return _checkSameOnBoardController.CheckSame();
        }
    }
}
