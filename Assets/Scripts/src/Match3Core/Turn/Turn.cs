using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.MakeTurn
{
    public class Turn
    {
        public Coordinate coordinate { get; set; }
        public Vector2 vector { get; set; }

        public Turn(Coordinate coordinate, Vector2 vector)
        {
            this.coordinate = coordinate;
            this.vector = vector;
        }
    }
}