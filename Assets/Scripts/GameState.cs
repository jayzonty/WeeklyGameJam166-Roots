using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WGJRoots
{
    public class GameState : MonoBehaviour
    {
        public UnityEvent onGameOver;
        public UnityEvent<int> onGrowthLevelIncreased;

        public int growthLevel = 0;

        public int NutrientPoints
        {
            get
            {
                return nutrientPoints;
            }

            set
            {
                nutrientPoints = value;
                if (nutrientPoints >= NutrientPointsToNextGrowthLevel)
                {
                    nutrientPoints -= NutrientPointsToNextGrowthLevel;
                    ++growthLevel;
                    NutrientPointsToNextGrowthLevel = CalculateRequiredNutrientPointsToNextLevel(growthLevel);

                    onGrowthLevelIncreased?.Invoke(growthLevel);
                }
            }
        }

        public int NutrientPointsToNextGrowthLevel
        {
            get;
            private set;
        }

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

        public void GameOver()
        {
            if (IsGameOver)
            {
                return;
            }

            IsGameOver = true;
            onGameOver?.Invoke();
        }

        private void Start()
        {
            branchPoints = initialBranchPoints;

            NutrientPointsToNextGrowthLevel = CalculateRequiredNutrientPointsToNextLevel(growthLevel);
        }

        private int nutrientPoints = 0;
        private int branchPoints = 0;

        private int CalculateRequiredNutrientPointsToNextLevel(int currentGrowthLevel)
        {
            // y = x^2 + 4
            return currentGrowthLevel * currentGrowthLevel + 4;
        }
    }
}
