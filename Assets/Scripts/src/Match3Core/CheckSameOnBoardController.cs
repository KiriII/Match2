using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Src.Match3Core
{
    public class CheckSameOnBoardController
    {
        private ITripleCellsCheckModel _match3Model;

        private CheckSameInCollumns _checkSameInCollumns;
        private CheckSameInRows _checkSameInRows;

        public CheckSameOnBoardController(Match3Model match3Model) 
        {
            _match3Model = match3Model;

            _checkSameInCollumns = new CheckSameInCollumns(match3Model);
            _checkSameInRows = new CheckSameInRows(match3Model);
        }

        public List<(int,int)> CheckSame()
        {
            var findedCells = new List<(int,int)>();

            _checkSameInCollumns.CheckSame(ref findedCells);
            _checkSameInRows.CheckSame(ref findedCells);

            return findedCells;
        }
    }
}
