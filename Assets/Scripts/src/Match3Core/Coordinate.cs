using System;
using UnityEngine;

namespace Match3Core
{
    public class Coordinate : IComparable<Coordinate>, IEquatable<Coordinate>
    {
        public int x;
        public int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Coordinate(Coordinate coordinate)
        {
            this.x = coordinate.x;
            this.y = coordinate.y;
        }

        public Coordinate(Coordinate coordinate, Vector2 vector)
        {
            this.x = coordinate.x - (int)vector.y;
            this.y = coordinate.y + (int)vector.x;
        }

        public int CompareTo(Coordinate other)
        {
            if (x == other.x) return y.CompareTo(other.y);
            return x.CompareTo(other.x);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Coordinate objAsPart = obj as Coordinate;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(Coordinate coordinate)
        {
            return (this.x == coordinate.x && this.y == coordinate.y && GetHashCode() == coordinate.GetHashCode());
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(x, y);
        }

        public override string ToString()
        {
            return $"[{x}:{y}]";
        }
    }
}
