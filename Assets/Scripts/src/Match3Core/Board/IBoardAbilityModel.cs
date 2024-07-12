using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Board
{
    public interface IBoardAbilityModel
    {
        public List<Coordinate> GetCoordinateSlotsWithCellsInRow(Coordinate coordinate);
        public List<Coordinate> GetCoordinateSlotsWithCellsInCollumn(Coordinate coordinate);
        public List<Coordinate> GetCoordinateSlotsWithCellsAround(Coordinate coordinate);
    }
}