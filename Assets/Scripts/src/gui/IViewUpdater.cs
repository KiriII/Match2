using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;

namespace Match3Core.gui
{
    public interface IViewUpdater
    {
        void UpdateView(Slot[,] boardCopy);
    }
}
