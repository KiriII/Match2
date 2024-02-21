using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.MakeTurn
{
    public class Turn
    {
        public Coordinate coordinate { get; set; }
        public Vector2 vector { get; set; }
        public Vector3 position { get; set; }
        public Slot fallenSlot { get; set; }
        public GameObject fallenSlotObject { get; set; }

        public Turn(Coordinate coordinate, Vector2 vector, Vector3 position, Slot fallenSlot = null, GameObject fallenSlotObject = null)
        {
            this.coordinate = coordinate;
            this.vector = vector;
            this.position = position;
            this.fallenSlot = fallenSlot;
            this.fallenSlotObject = fallenSlotObject;
        }

        public Turn(Coordinate coordinate, Slot fallenSlot, GameObject fallenSlotObject)
        {
            this.coordinate = coordinate;
            this.vector = Vector2.zero;
            this.position = Vector3.zero;
            this.fallenSlot = fallenSlot;
            this.fallenSlotObject = fallenSlotObject;
        }

        public override string ToString()
        {
            return $"[{coordinate}:{vector}:{position}:{fallenSlot}]";
        }
    }
}