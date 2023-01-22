using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core
{
    public class CheckSameInCollumns : CheckSameWithRule
    {
        public CheckSameInCollumns(ITripleCellsCheckModel match3Model) : base(match3Model) { }

        public override void CheckSame(ref List<(int, int)> findedCells)
        {
            var collumnsCount = _match3Model.GetColumnsCount();

            for (int collumnNumber = 0; collumnNumber < collumnsCount; collumnNumber++)
            {
                var cellsInCollumn = _match3Model.GetCollumnArray(collumnNumber);
                var findedCellsInCollumn = SameCellsFinder.CheckSameInArray(cellsInCollumn);
                foreach (var cell in findedCellsInCollumn)
                {
                    var newCell = (cell, collumnNumber);
                    if (findedCells.Contains(newCell)) continue;
                    findedCells.Add(newCell);
                }
            }
        }
    }
}
