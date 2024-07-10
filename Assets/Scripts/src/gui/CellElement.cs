using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.gui
{
    public class CellElement : UIComp
    {
        [SerializeField] public GameObject NormalState;
        [SerializeField] public GameObject SpecialState;

        public override void Validate()
        {
            AssertNotNull(NormalState, nameof(NormalState));
            AssertNotNull(SpecialState, nameof(SpecialState));
        }
    }
}