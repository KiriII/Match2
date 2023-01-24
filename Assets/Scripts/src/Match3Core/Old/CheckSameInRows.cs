using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core
{
    public class CheckSameInRows : CheckSameWithRule
    {
        public CheckSameInRows(ITripleCellsCheckModel match3Model) : base(match3Model) { }

        public override void CheckSame(ref List<(int, int)> findedCells)
        {
            var rowsCount = _match3Model.GetRowsCount();

            for (int rowNumber = 0; rowNumber < rowsCount; rowNumber++)
            {
                var cellsInRow = _match3Model.GetRowArray(rowNumber);
                var findedCellsInRow = SameCellsFinder.CheckSameInArray(cellsInRow);
                foreach (var cell in findedCellsInRow)
                {
                    var newCell = (rowNumber, cell);
                    if (findedCells.Contains(newCell)) continue;
                    findedCells.Add(newCell);
                }
            }
        }
    }
}
