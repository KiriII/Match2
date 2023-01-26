using System.Collections.Generic;
using Match3Core;
using Match3.Board;

using UnityEngine;

namespace Match3.Falling
{
    public class FallingLine
    {
        private List<Coordinate> _coordinates;
        private IFallLineModel _fallLineModel;

        public FallingLine(List<Coordinate> coordinate, IFallLineModel fallLineModel)
        {
            _coordinates = coordinate;
            _fallLineModel = fallLineModel;
        }

        public void Fall()
        {
            var oldLine = _fallLineModel.GetSomeCells(_coordinates);
            Debug.Log($"{string.Join(", ", oldLine)}");
            if (!oldLine.Contains(null)) return;
            var newLine = new List<Cell>(oldLine);
            Debug.Log($"{string.Join(", ", newLine)}");
            var deletedCellsCount = newLine.RemoveAll(cell => cell == null);
            for (int i = 0; i < deletedCellsCount; i++)
            {
                var newCell = GetNewCell();
                newLine.Insert(0, newCell);

            }
            Debug.Log($"{string.Join(", ", newLine)}");
            _fallLineModel.SetSomeCells(_coordinates, newLine);
        }

        // --------- need generator ----------
        private Cell GetNewCell()
        {
            return new Cell(CellsColor.Red);
        }
    }
}