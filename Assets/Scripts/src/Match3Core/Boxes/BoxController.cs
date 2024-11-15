using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;

namespace Match3Core.Box
{
    public class BoxController
    {
        private BoxModel _boxModel;
        private IBoardBoxModel _boardModel;

        private event Action<List<Coordinate>> _specialCellDroppedDown;

        public BoxController(BoxModel boxModel, IBoardBoxModel boardModel)
        {
            _boxModel = boxModel;
            _boardModel = boardModel;
        }

        public void FindBoxDropdown()
        {
            var findedDroppedCells = new List<Coordinate>();

            var boxCoordinates = _boxModel.GetCoordinates();
            foreach (var coordinate in boxCoordinates)
            {
                //if (coordinate.x <= 0) continue;
                var checkingCellCoordinate = new Coordinate(coordinate, Vector2.up);
                while (checkingCellCoordinate.x > 0)
				{
                    if (!_boardModel.GetCanHoldCell(checkingCellCoordinate))
                    {
                        checkingCellCoordinate = new Coordinate(checkingCellCoordinate, Vector2.up);
                        continue;
                    }
                    else
                    {
                        var dropDownCell = _boardModel.GetCell(checkingCellCoordinate);
                        if (!_boardModel.GetCanHoldCell(checkingCellCoordinate)) break;
                        if (dropDownCell.Color == CellsColor.Special)
                        {
                            findedDroppedCells.Add(checkingCellCoordinate);
                        }
                        break;
                    }
                }
            }
            if (findedDroppedCells.Count > 0)
            {
                OnSpecialCellDroppedDown(findedDroppedCells);
            }
        }

        private void OnSpecialCellDroppedDown(List<Coordinate> destroyedCells)
        {
            _specialCellDroppedDown?.Invoke(destroyedCells);
        }

        public void EnableSpecialCellDroppedDownListener(Action<List<Coordinate>> methodInLitener)
        {
            _specialCellDroppedDown += methodInLitener;
        }

        public void DesableSpecialCellDroppedDownListener(Action<List<Coordinate>> methodInLitener)
        {
            _specialCellDroppedDown -= methodInLitener;
        }
    }
}