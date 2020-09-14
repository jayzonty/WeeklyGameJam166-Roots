using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WGJRoots
{
    public class GameState : MonoBehaviour
    {
        public UnityEvent onGameOver;

        public int growthLevel;

        public int nutrientPoints;
        public int nutrientPointsToNextGrowthLevel;

        public int initialBranchPoints = 10;

        public bool IsGameOver
        {
            get;
            private set;
        } = false;

        public int BranchPoints
        {
            get
            {
                return branchPoints;
            }

            set
            {
                branchPoints = value;
                if (branchPoints <= 0)
                {
                    IsGameOver = true;

                    // Notify game over
                    onGameOver.Invoke();
                }
            }
        }

        private void Start()
        {
            branchPoints = initialBranchPoints;
        }

        private int branchPoints = 0;
    }
}
