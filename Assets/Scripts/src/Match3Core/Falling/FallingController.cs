using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;
using Match3Core;
using Match3Core.RandomGenerate;

namespace Match3Core.Falling
{
    public class FallingController
    {
        protected IFallLineModel _fallLineModel;
        protected SwitchCellsContoller _switchCellsContoller;

        private event Action _cellsFell;

        public virtual IFallLineModel FallLineModel { get => _fallLineModel; set => _fallLineModel = value; }
        public virtual SwitchCellsContoller SwitchCellsContoller { get => _switchCellsContoller; set => _switchCellsContoller = value; }

        public FallingController(IFallLineModel fallLineModel, SwitchCellsContoller switchCellsContoller)
        {
            FallLineModel = fallLineModel;
            SwitchCellsContoller = switchCellsContoller;
        }

        public virtual void FallingWithDeadCells(List<Coordinate> deadCellsCoordinates)
        {
            Debug.Log(String.Join(", ", deadCellsCoordinates));
            if (deadCellsCoordinates.Count > 0) 
            {
                var emptySlots = EmptyCellsFinder.FindEmpty(_fallLineModel.GetSlots());
                foreach (var slot in emptySlots)
                {
                    if (!deadCellsCoordinates.Contains(slot)) deadCellsCoordinates.Add(slot);
                }
            }
            SortDestroyedCells(ref deadCellsCoordinates);
            Debug.Log(String.Join(", ", deadCellsCoordinates));

            foreach (Coordinate c in deadCellsCoordinates)
            {
                TrainOfSteals(c);
            }
            if (deadCellsCoordinates.Count > 0) OnCellsFell();
        }

        private void SortDestroyedCells(ref List<Coordinate> deadCellsCoordinates)
        {
            deadCellsCoordinates.Sort();
        }

        public virtual void TrainOfSteals(Coordinate coordinate)
        {
            var currentCellCoordinate = coordinate;
            while (currentCellCoordinate.x >= 0)
            {
                var nextCellCoordinate = GetNextCell(currentCellCoordinate);
                //Debug.Log($"{currentCellCoordinate} {nextCellCoordinate}");
                if (nextCellCoordinate == null) break;
                if (nextCellCoordinate.x == -1)
                {
                    _switchCellsContoller.SwitchWithNewCell(currentCellCoordinate, RandomCellsGenerator.GenerateNewCell());
                    break;
                }
                _switchCellsContoller.SwitchCells(currentCellCoordinate, nextCellCoordinate);
                
                currentCellCoordinate = nextCellCoordinate;
            }
        }

        protected virtual Coordinate GetNextCell(Coordinate coordinate)
        {
            return GetCellFromAbove(coordinate);
        }

        protected virtual Coordinate GetCellFromAbove(Coordinate coordinate)
        {
            var newCoordinate = new Coordinate(coordinate.x - 1, coordinate.y);
            if (newCoordinate.x == -1) return newCoordinate;
            if (!_fallLineModel.GetCanHoldCell(newCoordinate))
            {
                if (_fallLineModel.GetCanPassCell(newCoordinate))
                {
                    return GetNextCell(newCoordinate);
                }
                else
                {
                    return null;
                }
            }
            if (_fallLineModel.GetBlocked(newCoordinate))
            {
                return null;
            }
            return newCoordinate;
        }

        private void OnCellsFell()
        {
            _cellsFell?.Invoke();
        }

        public void EnableCellsFellListener(Action methodInLitener)
        {
            _cellsFell += methodInLitener;
        }

        public void DesableCellsFellListener(Action methodInLitener)
        {
            _cellsFell -= methodInLitener;
        }
    }
}