using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.gui
{
    public class ViewUpdateStack
    {
        private float _timeToUpdate = 1;

        private Queue<Slot[,]> _boardScreens = new Queue<Slot[,]>();

        private IViewUpdater _view;

        public ViewUpdateStack(IViewUpdater view, Match3GameCore gameCore)
        {
            _view = view;
            gameCore.EnableBoardScreenListener(AddBoardScreens);

        }

        public void UpdateScreen()
        {
            if (_boardScreens.Count > 0)
            {
                _timeToUpdate -= Time.deltaTime;
                if (_timeToUpdate <= 0)
                {
                    _timeToUpdate = 1;
                    var boardScreen = _boardScreens.Dequeue();
                    UpdateView(boardScreen);
                }
            }
        }

        private void UpdateView(Slot[,] boardCopy)
        {
            _view.UpdateView(boardCopy);
        }

        private void AddBoardScreens(Slot[,] boardCopy)
        {
            _boardScreens.Enqueue(boardCopy);
        }
    }
}