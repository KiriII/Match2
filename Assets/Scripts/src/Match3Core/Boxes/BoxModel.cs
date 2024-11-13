using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Box
{
    public class BoxModel : IBoxWinConditionModel
    {
        private ICollection<Coordinate> _boxes;

        public BoxModel(int boardSize)
        {
            _boxes = new HashSet<Coordinate>();
            for (var i = 0; i < boardSize; i++)
			{
                _boxes.Add(new Coordinate(boardSize, i));
			}
        }

        public ICollection<Coordinate> GetCoordinates()
        {
            return _boxes;
        }

        public int GetSpecialCellsCount()
        {
            return _boxes.Count;
        }
    }
}
