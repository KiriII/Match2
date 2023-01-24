using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Match3Core
{
    public class FallCellsController
    {
        private ICellsFallModel _cellsFallModel;

        public FallCellsController(ICellsFallModel cellsFallModel)
        {
            _cellsFallModel = cellsFallModel;
        }

        public void CellsFall(int collumn, int cell)
        {
            _cellsFallModel.FallAliveCellsInCollumn(collumn, cell);
        }

        public void CellsFall((int collumn, int row) cellPosition)
        {
            _cellsFallModel.RaiseDeadCell(cellPosition.collumn, cellPosition.row);
            Debug.Log(_cellsFallModel.Print());
        }
    }
}
