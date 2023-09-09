using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Box
{
    public class BoxModel : IGUIBoxModel
    {
        private Dictionary<Coordinate, string> _boxes;

        public BoxModel(Dictionary<Coordinate, string> boxes)
        {
            _boxes = new Dictionary<Coordinate, string>(boxes);
        }

        public List<Coordinate> GetCoordinates()
        {
            return _boxes.Keys.ToList();
        }

        public string GetIdByCoordinate(Coordinate coordinate)
        {
            if (!_boxes.ContainsKey(coordinate)) return null;
            return _boxes[coordinate];
        }
    }
}
