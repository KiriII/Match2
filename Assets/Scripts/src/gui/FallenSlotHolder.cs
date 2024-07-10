using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.gui
{
    public class FallenSlotHolder : UIComp
    {
        public Slot fallenSlot;

        public override void Validate()
        {
            AssertNotNull(fallenSlot, nameof(fallenSlot));
        }
    }
}