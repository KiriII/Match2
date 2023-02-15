using System;

namespace Match3Core
{
    public class Coordinate : IComparable<Coordinate>
    {
        public int x;
        public int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int CompareTo(Coordinate other)
        {
            if (x == other.x) return y.CompareTo(other.y);
            return x.CompareTo(other.x);
        }

        public bool Equals(Coordinate coordinate)
        {
            return (this.x == coordinate.x && this.y == coordinate.y);
        }

        public override string ToString()
        {
            return $"[{x}:{y}]";
        }
    }
}
