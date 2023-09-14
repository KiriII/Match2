using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core
{
    public class ScoreHolder
    {
        private int Score;
        private int triplesInRow;

        public ScoreHolder()
        {
            Score = 0;
            triplesInRow = 0;
        }

        public void AddScore(int tripleCount)
        {
            triplesInRow += 1;
            var addedScore = 10 * tripleCount * triplesInRow + 5 * Math.Pow(tripleCount, 3);
            //Debug.Log($"{Score} += 10 * {tripleCount} * {triplesInRow} + 5 * {Math.Pow(tripleCount, 3)} \n {Score + addedScore}");
            Score += (int)addedScore;
        }

        public void ResetRowAfterTurn()
        {
            triplesInRow = 0;
        }

        public int GetScore()
        {
            return Score;
        }
    }
}