using UnityEngine;

namespace Match3Core
{
    public class Condition
    {
        public byte flags { get; set; }
        public CellsColor color { get; set; }
        public int colorCount { get; set; }

        public Condition(byte flags, CellsColor color = CellsColor.Empty, int colorCount = 0)
        {
            this.flags = flags;
            this.color = color;
            this.colorCount = colorCount;
        }

        public override string ToString()
        {
            return $"flags : {(ConditionFlags)flags}\n" +
                $"Color : {color} {colorCount}";
        }
    }
}