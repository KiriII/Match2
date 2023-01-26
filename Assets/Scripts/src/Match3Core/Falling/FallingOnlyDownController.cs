using System.Collections.Generic;
using Match3.Board;
using Match3Core;
using UnityEngine;

namespace Match3.Falling
{
    public class FallingOnlyDownController
    {
        private IFallLineModel _fallLineModel;
        private List<FallingLine> _fallingLines;
         
        public FallingOnlyDownController(IFallLineModel fallLineModel)
        {
            _fallLineModel = fallLineModel;
            _fallingLines = new List<FallingLine>();
            CreateLines();
            Debug.Log($"{string.Join(", " , _fallingLines)}");
        }

        private void CreateLines()
        {
            var rows = _fallLineModel.GetRows();
            var collumns = _fallLineModel.GetCollumns();

            for (int i = 0; i < collumns; i++)
            {
                var coordinatesForLine = new List<Coordinate>();
                for (int j = 0; j < rows; j++)
                {
                    var coordinate = new Coordinate(j, i);
                    if (_fallLineModel.GetCanHoldCell(coordinate))
                    {
                        coordinatesForLine.Add(coordinate);
                    }
                }
                _fallingLines.Add(new FallingLine(coordinatesForLine, _fallLineModel));
            }
        }

        public void FallAllLines()
        {
            foreach(var line in _fallingLines)
            {
                line.Fall();
            }
        }
    }
}