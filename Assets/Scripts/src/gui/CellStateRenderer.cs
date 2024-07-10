using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.gui
{
    public class CellStateRenderer : UIComp
    {
        public GameObject ColorCell;
        public GameObject BlockedCell;

        public override void Validate()
        {
            AssertNotNull(ColorCell, nameof(ColorCell));
            AssertNotNull(BlockedCell, nameof(BlockedCell));
        }
    }
}