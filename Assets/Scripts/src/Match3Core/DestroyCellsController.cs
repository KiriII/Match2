using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core
{
    public class DestroyCellsController
    {
        private ICellsDestroyModel _cellsDestroyModel;

        public DestroyCellsController(ICellsDestroyModel cellsDestroyModel)
        {
            _cellsDestroyModel = cellsDestroyModel; 
        }

        public void DestroyCell((int collumn, int row) cellPosition)
        {
            _cellsDestroyModel.DestroyCell(cellPosition.collumn, cellPosition.row);
            Debug.Log(_cellsDestroyModel.Print());
        }
    }
}
